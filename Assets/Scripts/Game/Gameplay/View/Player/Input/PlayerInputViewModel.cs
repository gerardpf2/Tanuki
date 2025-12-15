using Game.Gameplay.View.Player.Input.ActionHandlers;
using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel;
using Infrastructure.ModelViewViewModel.Examples.Button;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Player.Input
{
    public class PlayerInputViewModel : ViewModel
    {
        private IPlayerInputActionHandler _lockPlayerInputActionHandler;
        private IPlayerInputActionHandler _moveLeftPlayerInputActionHandler;
        private IPlayerInputActionHandler _moveRightPlayerInputActionHandler;
        private IPlayerInputActionHandler _rotatePlayerInputActionHandler;

        [NotNull] private readonly IBoundProperty<ButtonViewData> _lock = new BoundProperty<ButtonViewData>("LockButtonViewData");
        [NotNull] private readonly IBoundProperty<ButtonViewData> _moveLeft = new BoundProperty<ButtonViewData>("MoveLeftButtonViewData");
        [NotNull] private readonly IBoundProperty<ButtonViewData> _moveRight = new BoundProperty<ButtonViewData>("MoveRightButtonViewData");
        [NotNull] private readonly IBoundProperty<ButtonViewData> _rotate = new BoundProperty<ButtonViewData>("RotateButtonViewData");

        private void Awake()
        {
            InjectResolver.Resolve(this);

            InitializeBindings();
            AddBindings();
            SubscribeToEvents();

            UpdateLockEnabled();
            UpdateMoveLeftEnabled();
            UpdateMoveRightEnabled();
            UpdateRotateEnabled();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        public void Inject(
            [NotNull] IPlayerInputActionHandler lockPlayerInputActionHandler,
            [NotNull] IPlayerInputActionHandler moveLeftPlayerInputActionHandler,
            [NotNull] IPlayerInputActionHandler moveRightPlayerInputActionHandler,
            [NotNull] IPlayerInputActionHandler rotatePlayerInputActionHandler)
        {
            ArgumentNullException.ThrowIfNull(lockPlayerInputActionHandler);
            ArgumentNullException.ThrowIfNull(moveLeftPlayerInputActionHandler);
            ArgumentNullException.ThrowIfNull(moveRightPlayerInputActionHandler);
            ArgumentNullException.ThrowIfNull(rotatePlayerInputActionHandler);

            _lockPlayerInputActionHandler = lockPlayerInputActionHandler;
            _moveLeftPlayerInputActionHandler = moveLeftPlayerInputActionHandler;
            _moveRightPlayerInputActionHandler = moveRightPlayerInputActionHandler;
            _rotatePlayerInputActionHandler = rotatePlayerInputActionHandler;
        }

        private void InitializeBindings()
        {
            _lock.Value = new ButtonViewData(HandleLockClick);
            _moveLeft.Value = new ButtonViewData(HandleMoveLeftClick);
            _moveRight.Value = new ButtonViewData(HandleMoveRightClick);
            _rotate.Value = new ButtonViewData(HandleRotateClick);
        }

        private void AddBindings()
        {
            Add(_lock);
            Add(_moveLeft);
            Add(_moveRight);
            Add(_rotate);
        }

        private void SubscribeToEvents()
        {
            InvalidOperationException.ThrowIfNull(_lockPlayerInputActionHandler);
            InvalidOperationException.ThrowIfNull(_moveLeftPlayerInputActionHandler);
            InvalidOperationException.ThrowIfNull(_moveRightPlayerInputActionHandler);
            InvalidOperationException.ThrowIfNull(_rotatePlayerInputActionHandler);

            UnsubscribeFromEvents();

            _lockPlayerInputActionHandler.OnAvailableUpdated += HandleLockActionAvailableUpdated;
            _moveLeftPlayerInputActionHandler.OnAvailableUpdated += HandleMoveLeftActionAvailableUpdated;
            _moveRightPlayerInputActionHandler.OnAvailableUpdated += HandleMoveRightActionAvailableUpdated;
            _rotatePlayerInputActionHandler.OnAvailableUpdated += HandleRotateActionAvailableUpdated;
        }

        private void UnsubscribeFromEvents()
        {
            InvalidOperationException.ThrowIfNull(_lockPlayerInputActionHandler);
            InvalidOperationException.ThrowIfNull(_moveLeftPlayerInputActionHandler);
            InvalidOperationException.ThrowIfNull(_moveRightPlayerInputActionHandler);
            InvalidOperationException.ThrowIfNull(_rotatePlayerInputActionHandler);

            _lockPlayerInputActionHandler.OnAvailableUpdated -= HandleLockActionAvailableUpdated;
            _moveLeftPlayerInputActionHandler.OnAvailableUpdated -= HandleMoveLeftActionAvailableUpdated;
            _moveRightPlayerInputActionHandler.OnAvailableUpdated -= HandleMoveRightActionAvailableUpdated;
            _rotatePlayerInputActionHandler.OnAvailableUpdated -= HandleRotateActionAvailableUpdated;
        }

        private void HandleLockClick()
        {
            InvalidOperationException.ThrowIfNull(_lockPlayerInputActionHandler);

            _lockPlayerInputActionHandler.Resolve();
        }

        private void HandleMoveLeftClick()
        {
            InvalidOperationException.ThrowIfNull(_moveLeftPlayerInputActionHandler);

            _moveLeftPlayerInputActionHandler.Resolve();
        }

        private void HandleMoveRightClick()
        {
            InvalidOperationException.ThrowIfNull(_moveRightPlayerInputActionHandler);

            _moveRightPlayerInputActionHandler.Resolve();
        }

        private void HandleRotateClick()
        {
            InvalidOperationException.ThrowIfNull(_rotatePlayerInputActionHandler);

            _rotatePlayerInputActionHandler.Resolve();
        }

        private void HandleLockActionAvailableUpdated()
        {
            UpdateLockEnabled();
        }

        private void HandleMoveLeftActionAvailableUpdated()
        {
            UpdateMoveLeftEnabled();
        }

        private void HandleMoveRightActionAvailableUpdated()
        {
            UpdateMoveRightEnabled();
        }

        private void HandleRotateActionAvailableUpdated()
        {
            UpdateRotateEnabled();
        }

        private void UpdateLockEnabled()
        {
            InvalidOperationException.ThrowIfNull(_lockPlayerInputActionHandler);
            InvalidOperationException.ThrowIfNull(_lock.Value);

            UpdateButtonEnabled(_lock.Value, _lockPlayerInputActionHandler);
        }

        private void UpdateMoveLeftEnabled()
        {
            InvalidOperationException.ThrowIfNull(_moveLeftPlayerInputActionHandler);
            InvalidOperationException.ThrowIfNull(_moveLeft.Value);

            UpdateButtonEnabled(_moveLeft.Value, _moveLeftPlayerInputActionHandler);
        }

        private void UpdateMoveRightEnabled()
        {
            InvalidOperationException.ThrowIfNull(_moveRightPlayerInputActionHandler);
            InvalidOperationException.ThrowIfNull(_moveRight.Value);

            UpdateButtonEnabled(_moveRight.Value, _moveRightPlayerInputActionHandler);
        }

        private void UpdateRotateEnabled()
        {
            InvalidOperationException.ThrowIfNull(_rotatePlayerInputActionHandler);
            InvalidOperationException.ThrowIfNull(_rotate.Value);

            UpdateButtonEnabled(_rotate.Value, _rotatePlayerInputActionHandler);
        }

        private static void UpdateButtonEnabled(
            [NotNull] ButtonViewData buttonViewData,
            [NotNull] IPlayerInputActionHandler playerInputActionHandler)
        {
            ArgumentNullException.ThrowIfNull(buttonViewData);
            ArgumentNullException.ThrowIfNull(playerInputActionHandler);

            buttonViewData.SetEnabled(playerInputActionHandler.Available);
        }
    }
}