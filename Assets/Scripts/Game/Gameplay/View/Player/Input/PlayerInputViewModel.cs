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
        private IPlayerInputActionHandler _swapCurrentNextPlayerInputActionHandler;

        [NotNull] private readonly IBoundProperty<ButtonViewData> _lock = new BoundProperty<ButtonViewData>("LockButtonViewData");
        [NotNull] private readonly IBoundProperty<ButtonViewData> _moveLeft = new BoundProperty<ButtonViewData>("MoveLeftButtonViewData");
        [NotNull] private readonly IBoundProperty<ButtonViewData> _moveRight = new BoundProperty<ButtonViewData>("MoveRightButtonViewData");
        [NotNull] private readonly IBoundProperty<ButtonViewData> _rotate = new BoundProperty<ButtonViewData>("RotateButtonViewData");
        [NotNull] private readonly IBoundProperty<ButtonViewData> _swapCurrentNext = new BoundProperty<ButtonViewData>("SwapCurrentNextButtonViewData");

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
            UpdateSwapCurrentNextEnabled();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        public void Inject(
            [NotNull] IPlayerInputActionHandler lockPlayerInputActionHandler,
            [NotNull] IPlayerInputActionHandler moveLeftPlayerInputActionHandler,
            [NotNull] IPlayerInputActionHandler moveRightPlayerInputActionHandler,
            [NotNull] IPlayerInputActionHandler rotatePlayerInputActionHandler,
            [NotNull] IPlayerInputActionHandler swapCurrentNextPlayerInputActionHandler)
        {
            ArgumentNullException.ThrowIfNull(lockPlayerInputActionHandler);
            ArgumentNullException.ThrowIfNull(moveLeftPlayerInputActionHandler);
            ArgumentNullException.ThrowIfNull(moveRightPlayerInputActionHandler);
            ArgumentNullException.ThrowIfNull(rotatePlayerInputActionHandler);
            ArgumentNullException.ThrowIfNull(swapCurrentNextPlayerInputActionHandler);

            _lockPlayerInputActionHandler = lockPlayerInputActionHandler;
            _moveLeftPlayerInputActionHandler = moveLeftPlayerInputActionHandler;
            _moveRightPlayerInputActionHandler = moveRightPlayerInputActionHandler;
            _rotatePlayerInputActionHandler = rotatePlayerInputActionHandler;
            _swapCurrentNextPlayerInputActionHandler = swapCurrentNextPlayerInputActionHandler;
        }

        private void InitializeBindings()
        {
            _lock.Value = new ButtonViewData(HandleLockClick);
            _moveLeft.Value = new ButtonViewData(HandleMoveLeftClick);
            _moveRight.Value = new ButtonViewData(HandleMoveRightClick);
            _rotate.Value = new ButtonViewData(HandleRotateClick);
            _swapCurrentNext.Value = new ButtonViewData(HandleSwapCurrentNextClick);
        }

        private void AddBindings()
        {
            Add(_lock);
            Add(_moveLeft);
            Add(_moveRight);
            Add(_rotate);
            Add(_swapCurrentNext);
        }

        private void SubscribeToEvents()
        {
            InvalidOperationException.ThrowIfNull(_lockPlayerInputActionHandler);
            InvalidOperationException.ThrowIfNull(_moveLeftPlayerInputActionHandler);
            InvalidOperationException.ThrowIfNull(_moveRightPlayerInputActionHandler);
            InvalidOperationException.ThrowIfNull(_rotatePlayerInputActionHandler);
            InvalidOperationException.ThrowIfNull(_swapCurrentNextPlayerInputActionHandler);

            UnsubscribeFromEvents();

            _lockPlayerInputActionHandler.OnAvailableUpdated += HandleLockActionAvailableUpdated;
            _moveLeftPlayerInputActionHandler.OnAvailableUpdated += HandleMoveLeftActionAvailableUpdated;
            _moveRightPlayerInputActionHandler.OnAvailableUpdated += HandleMoveRightActionAvailableUpdated;
            _rotatePlayerInputActionHandler.OnAvailableUpdated += HandleRotateActionAvailableUpdated;
            _swapCurrentNextPlayerInputActionHandler.OnAvailableUpdated += HandleSwapCurrentNextActionAvailableUpdated;
        }

        private void UnsubscribeFromEvents()
        {
            InvalidOperationException.ThrowIfNull(_lockPlayerInputActionHandler);
            InvalidOperationException.ThrowIfNull(_moveLeftPlayerInputActionHandler);
            InvalidOperationException.ThrowIfNull(_moveRightPlayerInputActionHandler);
            InvalidOperationException.ThrowIfNull(_rotatePlayerInputActionHandler);
            InvalidOperationException.ThrowIfNull(_swapCurrentNextPlayerInputActionHandler);

            _lockPlayerInputActionHandler.OnAvailableUpdated -= HandleLockActionAvailableUpdated;
            _moveLeftPlayerInputActionHandler.OnAvailableUpdated -= HandleMoveLeftActionAvailableUpdated;
            _moveRightPlayerInputActionHandler.OnAvailableUpdated -= HandleMoveRightActionAvailableUpdated;
            _rotatePlayerInputActionHandler.OnAvailableUpdated -= HandleRotateActionAvailableUpdated;
            _swapCurrentNextPlayerInputActionHandler.OnAvailableUpdated -= HandleSwapCurrentNextActionAvailableUpdated;
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

        private void HandleSwapCurrentNextClick()
        {
            InvalidOperationException.ThrowIfNull(_swapCurrentNextPlayerInputActionHandler);

            _swapCurrentNextPlayerInputActionHandler.Resolve();
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

        private void HandleSwapCurrentNextActionAvailableUpdated()
        {
            UpdateSwapCurrentNextEnabled();
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

        private void UpdateSwapCurrentNextEnabled()
        {
            InvalidOperationException.ThrowIfNull(_swapCurrentNextPlayerInputActionHandler);
            InvalidOperationException.ThrowIfNull(_swapCurrentNext.Value);

            UpdateButtonEnabled(_swapCurrentNext.Value, _swapCurrentNextPlayerInputActionHandler);
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