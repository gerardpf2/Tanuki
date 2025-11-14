using System;
using Game.Common;
using Game.Gameplay.Phases;
using Game.Gameplay.View.EventResolvers;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.Player.Input.ActionHandlers
{
    public abstract class BasePlayerInputActionHandler : IPlayerInputActionHandler
    {
        [NotNull] private readonly IPhaseContainer _phaseContainer;
        [NotNull] private readonly IEventsResolver _eventsResolver;
        [NotNull] private readonly IPlayerPieceGhostView _playerPieceGhostView;
        [NotNull] private readonly IPlayerPieceView _playerPieceView;

        private InitializedLabel _initializedLabel;

        private bool _available;

        public event Action OnAvailableUpdated;

        public bool Available
        {
            get => _available;
            private set
            {
                if (Available == value)
                {
                    return;
                }

                _available = value;

                OnAvailableUpdated?.Invoke();
            }
        }

        protected BasePlayerInputActionHandler(
            [NotNull] IPhaseContainer phaseContainer,
            [NotNull] IEventsResolver eventsResolver,
            [NotNull] IPlayerPieceGhostView playerPieceGhostView,
            [NotNull] IPlayerPieceView playerPieceView)
        {
            ArgumentNullException.ThrowIfNull(phaseContainer);
            ArgumentNullException.ThrowIfNull(eventsResolver);
            ArgumentNullException.ThrowIfNull(playerPieceGhostView);
            ArgumentNullException.ThrowIfNull(playerPieceView);

            _phaseContainer = phaseContainer;
            _eventsResolver = eventsResolver;
            _playerPieceGhostView = playerPieceGhostView;
            _playerPieceView = playerPieceView;
        }

        public void Initialize()
        {
            _initializedLabel.SetInitialized();

            SubscribeToEvents();
            UpdateAvailable();
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();

            UnsubscribeFromEvents();
        }

        public void Resolve()
        {
            if (!Available)
            {
                InvalidOperationException.Throw("Cannot be resolved because it is not available");
            }

            ResolveImpl();

            _phaseContainer.Resolve(GetResolveContext());
        }

        private void SubscribeToEvents()
        {
            UnsubscribeFromEvents();

            _eventsResolver.OnResolveBegin += HandleEventInvoked;
            _eventsResolver.OnResolveEnd += HandleEventInvoked;
            _playerPieceView.OnInstantiated += HandleEventInvoked;
            _playerPieceView.OnDestroyed += HandleEventInvoked;
        }

        private void UnsubscribeFromEvents()
        {
            _eventsResolver.OnResolveBegin -= HandleEventInvoked;
            _eventsResolver.OnResolveEnd -= HandleEventInvoked;
            _playerPieceView.OnInstantiated -= HandleEventInvoked;
            _playerPieceView.OnDestroyed -= HandleEventInvoked;
        }

        private void HandleEventInvoked()
        {
            UpdateAvailable();
        }

        private void UpdateAvailable()
        {
            Available = GetAvailable();
        }

        protected virtual bool GetAvailable()
        {
            return
                !_eventsResolver.Resolving &&
                _playerPieceGhostView.Instance != null &&
                _playerPieceView.Instance != null;
        }

        protected virtual void ResolveImpl() { }

        [NotNull]
        private ResolveContext GetResolveContext()
        {
            return new ResolveContext(_playerPieceView.Coordinate, _playerPieceGhostView.Coordinate);
        }
    }
}