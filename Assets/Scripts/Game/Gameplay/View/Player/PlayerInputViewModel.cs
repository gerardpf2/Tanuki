using Game.Common.UI;
using Game.Gameplay.Phases;
using Game.Gameplay.Phases.Phases;
using Game.Gameplay.Phases.Utils;
using Game.Gameplay.View.EventResolvers;
using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Player
{
    public class PlayerInputViewModel : ViewModel
    {
        private IPhaseContainer _phaseContainer;
        private IPhaseResolver _phaseResolver;
        private IEventsResolver _eventsResolver;
        private IPhase _cameraTargetDesiredRowPhase;
        private IPlayerPieceGhostView _playerPieceGhostView;
        private IPlayerPieceView _playerPieceView;

        [NotNull] private readonly IBoundProperty<ButtonViewData> _moveLeft = new BoundProperty<ButtonViewData>("MoveLeftButtonViewData");
        [NotNull] private readonly IBoundProperty<ButtonViewData> _moveRight = new BoundProperty<ButtonViewData>("MoveRightButtonViewData");
        [NotNull] private readonly IBoundProperty<ButtonViewData> _rotate = new BoundProperty<ButtonViewData>("RotateButtonViewData");
        [NotNull] private readonly IBoundProperty<ButtonViewData> _lock = new BoundProperty<ButtonViewData>("LockButtonViewData");

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
            [NotNull] IPhaseContainer phaseContainer,
            [NotNull] IPhaseResolver phaseResolver,
            [NotNull] IEventsResolver eventsResolver,
            [NotNull] IPhase cameraTargetDesiredRowPhase,
            [NotNull] IPlayerPieceGhostView playerPieceGhostView,
            [NotNull] IPlayerPieceView playerPieceView)
        {
            ArgumentNullException.ThrowIfNull(phaseContainer);
            ArgumentNullException.ThrowIfNull(phaseResolver);
            ArgumentNullException.ThrowIfNull(eventsResolver);
            ArgumentNullException.ThrowIfNull(cameraTargetDesiredRowPhase);
            ArgumentNullException.ThrowIfNull(playerPieceGhostView);
            ArgumentNullException.ThrowIfNull(playerPieceView);

            _phaseContainer = phaseContainer;
            _phaseResolver = phaseResolver;
            _eventsResolver = eventsResolver;
            _cameraTargetDesiredRowPhase = cameraTargetDesiredRowPhase;
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

            UnsubscribeFromEvents();

            _eventsResolver.OnResolveBegin += OnResolveBegin;
            _eventsResolver.OnResolveEnd += OnResolveEnd;
        }

        private void UnsubscribeFromEvents()
        {
            InvalidOperationException.ThrowIfNull(_eventsResolver);

            _eventsResolver.OnResolveBegin -= OnResolveBegin;
            _eventsResolver.OnResolveEnd -= OnResolveEnd;
        }

        private void OnMoveLeftClick()
        {
            InvalidOperationException.ThrowIfNull(_playerPieceView);

            _playerPieceView.Move(-1.0f);

            ResolveAfterMove();
        }

        private void OnMoveRightClick()
        {
            InvalidOperationException.ThrowIfNull(_playerPieceView);

            _playerPieceView.Move(1.0f);

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
            InvalidOperationException.ThrowIfNull(_phaseContainer);

            _phaseContainer.Resolve(GetResolveContext(true));
        }

        private void OnResolveBegin()
        {
            SetButtonsEnabled(false);
        }

        private void OnResolveEnd()
        {
            SetButtonsEnabled(true);
        }

        private void SetButtonsEnabled(bool value)
        {
            _moveLeft.Value.SetEnabled(value);
            _moveRight.Value.SetEnabled(value);
            _rotate.Value.SetEnabled(value);
            _lock.Value.SetEnabled(value);
        }

        private void ResolveAfterMove()
        {
            InvalidOperationException.ThrowIfNull(_phaseResolver);

            _phaseResolver.Resolve(_cameraTargetDesiredRowPhase, GetResolveContext(false));
        }

        [NotNull]
        private ResolveContext GetResolveContext(bool comesFromLock)
        {
            InvalidOperationException.ThrowIfNull(_phaseContainer);
            InvalidOperationException.ThrowIfNull(_playerPieceGhostView);

            return new ResolveContext(comesFromLock, _playerPieceView.Coordinate, _playerPieceGhostView.Coordinate);
        }
    }
}