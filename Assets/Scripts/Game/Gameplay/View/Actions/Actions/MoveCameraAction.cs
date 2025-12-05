using System;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.View.Animation.Movement;
using Game.Gameplay.View.Camera;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.Actions.Actions
{
    public class MoveCameraAction : IAction
    {
        [NotNull] private readonly IMovementHelper _movementHelper;
        [NotNull] private readonly ICameraView _cameraView;
        private readonly int _rowOffset;
        private readonly MoveCameraReason _moveCameraReason;

        public MoveCameraAction(
            [NotNull] IMovementHelper movementHelper,
            [NotNull] ICameraView cameraView,
            int rowOffset,
            MoveCameraReason moveCameraReason)
        {
            ArgumentNullException.ThrowIfNull(movementHelper);
            ArgumentNullException.ThrowIfNull(cameraView);

            _movementHelper = movementHelper;
            _cameraView = cameraView;
            _rowOffset = rowOffset;
            _moveCameraReason = moveCameraReason;
        }

        public void Resolve(Action onComplete)
        {
            // TODO: Reason

            Transform transform = _cameraView.UnityCamera.transform;

            _movementHelper.DoCameraMovement(transform, _rowOffset, onComplete);
        }
    }
}