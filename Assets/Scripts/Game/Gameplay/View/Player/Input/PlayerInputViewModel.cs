using Game.Common.UI;
using Game.Gameplay.Phases;
using Game.Gameplay.View.EventResolvers;
using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Player.Input
{
    public class PlayerInputViewModel : ViewModel
    {
        private IPhaseContainer _phaseContainerLock;
        private IPhaseContainer _phaseContainerMove;
        private IEventsResolver _eventsResolver;
        private IPlayerPieceGhostView _playerPieceGhostView;
        private IPlayerPieceView _playerPieceView;

        [NotNull] private readonly IBoundProperty<ButtonViewData> _moveLeft = new BoundProperty<ButtonViewData>("MoveLeftButtonViewData");
        [NotNull] private readonly IBoundProperty<ButtonViewData> _moveRight = new BoundProperty<ButtonViewData>("MoveRightButtonViewData");
        [NotNull] private readonly IBoundProperty<ButtonViewData> _rotate = new BoundProperty<ButtonViewData>("RotateButtonViewData");
        [NotNull] private readonly IBoundProperty<ButtonViewData> _lock = new BoundProperty<ButtonViewData>("LockButtonViewData");

        private bool _resolvingEvents;
        private bool _playerPieceViewInstantiated;

        protected override void Awake()
        {
            base.Awake();

            InjectResolver.Resolve(this);

            InitializeBindings();
            AddBindings();
            SubscribeToEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        public void Inject(
            [NotNull] IPhaseContainer phaseContainerLock,
            [NotNull] IPhaseContainer phaseContainerMove,
            [NotNull] IEventsResolver eventsResolver,
            [NotNull] IPlayerPieceGhostView playerPieceGhostView,
            [NotNull] IPlayerPieceView playerPieceView)
        {
            ArgumentNullException.ThrowIfNull(phaseContainerLock);
            ArgumentNullException.ThrowIfNull(phaseContainerMove);
            ArgumentNullException.ThrowIfNull(eventsResolver);
            ArgumentNullException.ThrowIfNull(playerPieceGhostView);
            ArgumentNullException.ThrowIfNull(playerPieceView);

            _phaseContainerLock = phaseContainerLock;
            _phaseContainerMove = phaseContainerMove;
            _eventsResolver = eventsResolver;
            _playerPieceGhostView = playerPieceGhostView;
            _playerPieceView = playerPieceView;
        }

        private void InitializeBindings()
        {
            _moveLeft.Value = new ButtonViewData(OnMoveLeftClick);
            _moveRight.Value = new ButtonViewData(OnMoveRightClick);
            _rotate.Value = new ButtonViewData(OnRotateClick);
            _lock.Value = new ButtonViewData(OnLockClick);
        }

        private void AddBindings()
        {
            Add(_moveLeft);
            Add(_moveRight);
            Add(_rotate);
            Add(_lock);
        }

        private void SubscribeToEvents()
        {
            InvalidOperationException.ThrowIfNull(_eventsResolver);
            InvalidOperationException.ThrowIfNull(_playerPieceView);

            UnsubscribeFromEvents();

            _eventsResolver.OnResolveBegin += HandleResolveBegin;
            _eventsResolver.OnResolveEnd += HandleResolveEnd;
            _playerPieceView.OnInstantiated += HandlePlayerPieceViewInstantiated;
            _playerPieceView.OnDestroyed += HandlePlayerPieceViewDestroyed;
        }

        private void UnsubscribeFromEvents()
        {
            InvalidOperationException.ThrowIfNull(_eventsResolver);
            InvalidOperationException.ThrowIfNull(_playerPieceView);

            _eventsResolver.OnResolveBegin -= HandleResolveBegin;
            _eventsResolver.OnResolveEnd -= HandleResolveEnd;
            _playerPieceView.OnInstantiated -= HandlePlayerPieceViewInstantiated;
            _playerPieceView.OnDestroyed -= HandlePlayerPieceViewDestroyed;
        }

        private void OnMoveLeftClick()
        {
            const int offsetX = -1;

            InvalidOperationException.ThrowIfNull(_playerPieceView);

            _playerPieceView.Move(offsetX);

            ResolveAfterMove();
        }

        private void OnMoveRightClick()
        {
            const int offsetX = 1;

            InvalidOperationException.ThrowIfNull(_playerPieceView);

            _playerPieceView.Move(offsetX);

            ResolveAfterMove();
        }

        private void OnRotateClick()
        {
            InvalidOperationException.ThrowIfNull(_playerPieceView);

            _playerPieceView.Rotate();

            ResolveAfterMove();
        }

        private void OnLockClick()
        {
            InvalidOperationException.ThrowIfNull(_phaseContainerLock);

            _phaseContainerLock.Resolve(GetResolveContext());
        }

        private void HandleResolveBegin()
        {
            _resolvingEvents = true;

            RefreshButtonsEnabled();
        }

        private void HandleResolveEnd()
        {
            _resolvingEvents = false;

            RefreshButtonsEnabled();
        }

        private void HandlePlayerPieceViewInstantiated()
        {
            _playerPieceViewInstantiated = true;

            RefreshButtonsEnabled();
        }

        private void HandlePlayerPieceViewDestroyed()
        {
            _playerPieceViewInstantiated = false;

            RefreshButtonsEnabled();
        }

        private void RefreshButtonsEnabled()
        {
            bool buttonsEnabled = !_resolvingEvents && _playerPieceViewInstantiated;

            _moveLeft.Value.SetEnabled(buttonsEnabled);
            _moveRight.Value.SetEnabled(buttonsEnabled);
            _rotate.Value.SetEnabled(buttonsEnabled);
            _lock.Value.SetEnabled(buttonsEnabled);
        }

        private void ResolveAfterMove()
        {
            InvalidOperationException.ThrowIfNull(_phaseContainerMove);

            _phaseContainerMove.Resolve(GetResolveContext());
        }

        [NotNull]
        private ResolveContext GetResolveContext()
        {
            InvalidOperationException.ThrowIfNull(_playerPieceGhostView);
            InvalidOperationException.ThrowIfNull(_playerPieceView);

            return new ResolveContext(_playerPieceView.Coordinate, _playerPieceGhostView.Coordinate);
        }
    }
}