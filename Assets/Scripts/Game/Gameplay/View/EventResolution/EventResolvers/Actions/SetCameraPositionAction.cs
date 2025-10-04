using System;
using Game.Gameplay.View.Camera;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.EventResolution.EventResolvers.Actions
{
    public class SetCameraPositionAction : IAction
    {
        [NotNull] private readonly ICameraView _cameraView;
        private readonly int _topRow;

        public SetCameraPositionAction([NotNull] ICameraView cameraView, int topRow)
        {
            ArgumentNullException.ThrowIfNull(cameraView);

            _cameraView = cameraView;
            _topRow = topRow;
        }

        public void Resolve(Action onComplete)
        {
            _cameraView.UpdatePositionY(_topRow);

            onComplete?.Invoke(); // TODO: UpdatePosition param ¿?
        }
    }
}