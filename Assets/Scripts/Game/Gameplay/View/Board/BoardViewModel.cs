using Game.Gameplay.View.Camera;
using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity.Utils;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    public class BoardViewModel : ViewModel, IDataSettable<BoardViewData>
    {
        [SerializeField] private GameObject _piecesParentPrefab;
        [SerializeField] private Transform _top;
        [SerializeField] private Transform _bottom;

        private IBoardView _boardView;
        private ICameraView _cameraView;

        private void Awake()
        {
            InjectResolver.Resolve(this);
        }

        public void Inject([NotNull] IBoardView boardView, [NotNull] ICameraView cameraView)
        {
            ArgumentNullException.ThrowIfNull(boardView);
            ArgumentNullException.ThrowIfNull(cameraView);

            _boardView = boardView;
            _cameraView = cameraView;
        }

        public void SetData([NotNull] BoardViewData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            // Wait for end of frame so UI stuff has been properly updated
            // For example, CameraView will move camera to (0, 0) but UI refreshes later

            this.WaitForEndOfFrame(InitializeAndOnReadyInvoke);

            return;

            void InitializeAndOnReadyInvoke()
            {
                InstantiatePiecesParent();
                SetViewLimits();

                data.OnReady?.Invoke();
            }
        }

        private void InstantiatePiecesParent()
        {
            InvalidOperationException.ThrowIfNull(_boardView);

            _boardView.InstantiatePiecesParent(_piecesParentPrefab);
        }

        private void SetViewLimits()
        {
            InvalidOperationException.ThrowIfNull(_top);
            InvalidOperationException.ThrowIfNull(_bottom);
            InvalidOperationException.ThrowIfNull(_cameraView);

            _cameraView.SetBoardViewLimits(_top.position.y, _bottom.position.y);
        }
    }
}