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
        private readonly int _bottomRow;

        public SetCameraPositionAction([NotNull] ICameraView cameraView, int topRow, int bottomRow)
        {
            ArgumentNullException.ThrowIfNull(cameraView);

            _cameraView = cameraView;
            _topRow = topRow;
            _bottomRow = bottomRow;
        }

        public void Resolve(Action onComplete)
        {
            _cameraView.UpdatePosition(_topRow, _bottomRow);

            onComplete?.Invoke(); // TODO: UpdatePosition param ¿?
        }
    }
}