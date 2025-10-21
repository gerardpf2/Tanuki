using System;
using Game.Gameplay.View.Animation.Movement;
using Game.Gameplay.View.Camera;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.EventResolution.EventResolvers.Actions
{
    public class SetCameraRowAction : IAction
    {
        [NotNull] private readonly IMovementHelper _movementHelper;
        [NotNull] private readonly ICameraView _cameraView;
        private readonly int _row;

        public SetCameraRowAction([NotNull] IMovementHelper movementHelper, [NotNull] ICameraView cameraView, int row)
        {
            ArgumentNullException.ThrowIfNull(movementHelper);
            ArgumentNullException.ThrowIfNull(cameraView);

            _movementHelper = movementHelper;
            _cameraView = cameraView;
            _row = row;
        }

        public void Resolve(Action onComplete)
        {
            Transform transform = _cameraView.UnityCamera.transform;

            _movementHelper.DoCameraMovement(transform, _row, onComplete);
        }
    }
}