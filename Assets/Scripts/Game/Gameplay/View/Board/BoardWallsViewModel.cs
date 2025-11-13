using Game.Gameplay.Board;
using Game.Gameplay.View.Camera;
using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    public class BoardWallsViewModel : ViewModel
    {
        [SerializeField] private float _referenceHeight = 1920.0f;

        [NotNull] private readonly IBoundProperty<bool> _widthUpdated = new BoundProperty<bool>("WidthUpdated");
        [NotNull] private readonly IBoundProperty<Vector2> _offsetLeft = new BoundProperty<Vector2>("OffsetLeft");
        [NotNull] private readonly IBoundProperty<Vector2> _offsetRight = new BoundProperty<Vector2>("OffsetRight");

        private IBoard _board;
        private ICameraView _cameraView;
        private ICameraGetter _cameraGetter;

        protected override void Awake()
        {
            base.Awake();

            InjectResolver.Resolve(this);

            Add(_widthUpdated);
            Add(_offsetLeft);
            Add(_offsetRight);

            SubscribeToEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        public void Inject(
            [NotNull] IBoard board,
            [NotNull] ICameraView cameraView,
            [NotNull] ICameraGetter cameraGetter)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(cameraView);
            ArgumentNullException.ThrowIfNull(cameraGetter);

            _board = board;
            _cameraView = cameraView;
            _cameraGetter = cameraGetter;
        }

        private void SubscribeToEvents()
        {
            InvalidOperationException.ThrowIfNull(_cameraView);

            UnsubscribeFromEvents();

            _cameraView.OnUnityCameraSizeUpdated += HandleUnityCameraSizeUpdated;
        }

        private void UnsubscribeFromEvents()
        {
            InvalidOperationException.ThrowIfNull(_cameraView);

            _cameraView.OnUnityCameraSizeUpdated -= HandleUnityCameraSizeUpdated;
        }

        private void HandleUnityCameraSizeUpdated()
        {
            UpdateWidth();
        }

        private void UpdateWidth()
        {
            InvalidOperationException.ThrowIfNull(_board);
            InvalidOperationException.ThrowIfNull(_cameraGetter);

            UnityEngine.Camera unityCamera = _cameraGetter.GetMain();

            float unitsAmount = 2 * unityCamera.orthographicSize;
            float unitSize = _referenceHeight / unitsAmount;
            float boardWidth = _board.Columns * unitSize;
            float boardHalfWidth = 0.5f * boardWidth;

            _offsetLeft.Value = new Vector2(-boardHalfWidth, 0.0f);
            _offsetRight.Value = new Vector2(boardHalfWidth, 0.0f);

            _widthUpdated.Value = true;
        }
    }
}