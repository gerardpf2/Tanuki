using Game.Common.UI;
using Infrastructure.ModelViewViewModel;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Player
{
    public class PlayerInputViewModel : ViewModel
    {
        [NotNull] private readonly IBoundProperty<ButtonViewData> _moveLeft = new BoundProperty<ButtonViewData>("MoveLeftButtonViewData");
        [NotNull] private readonly IBoundProperty<ButtonViewData> _moveRight = new BoundProperty<ButtonViewData>("MoveRightButtonViewData");
        [NotNull] private readonly IBoundProperty<ButtonViewData> _rotate = new BoundProperty<ButtonViewData>("RotateButtonViewData");
        [NotNull] private readonly IBoundProperty<ButtonViewData> _lock = new BoundProperty<ButtonViewData>("LockButtonViewData");

        protected override void Awake()
        {
            base.Awake();

            InitializeBindings();
            AddBindings();
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

        private void OnMoveLeftClick()
        {
            // TODO

            Debug.Log("OnMoveLeftClick");
        }

        private void OnMoveRightClick()
        {
            // TODO

            Debug.Log("OnMoveRightClick");
        }

        private void OnRotateClick()
        {
            // TODO

            Debug.Log("OnRotateClick");
        }

        private void OnLockClick()
        {
            // TODO

            Debug.Log("OnLockClick");
        }
    }
}