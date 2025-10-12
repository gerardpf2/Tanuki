using System;
using Game.Gameplay.View.Camera;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.EventResolution.EventResolvers.Actions
{
    public class SetCameraRowAction : IAction
    {
        [NotNull] private readonly ICameraView _cameraView;
        private readonly int _row;

        public SetCameraRowAction([NotNull] ICameraView cameraView, int row)
        {
            ArgumentNullException.ThrowIfNull(cameraView);

            _cameraView = cameraView;
            _row = row;
        }

        public void Resolve(Action onComplete)
        {
            // TODO: Tween

            _cameraView.UpdatePositionY(_row);

            onComplete?.Invoke();
        }
    }
}