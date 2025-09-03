using Game.Gameplay.Board;
using Game.Gameplay.View.Camera;
using Game.Gameplay.View.EventResolution;
using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    public class BoardViewModel : ViewModel, IDataSettable<BoardViewData>
    {
        [SerializeField] private Transform _top;
        [SerializeField] private Transform _bottom;

        private IBoardController _boardController;
        private IBoardViewController _boardViewController;
        private ICameraController _cameraController;
        private IEventListener _eventListener;

        protected override void Awake()
        {
            base.Awake();

            InjectResolver.Resolve(this);
        }

        public void Inject(
            [NotNull] IBoardController boardController,
            [NotNull] IBoardViewController boardViewController,
            [NotNull] ICameraController cameraController,
            [NotNull] IEventListener eventListener)
        {
            ArgumentNullException.ThrowIfNull(boardController);
            ArgumentNullException.ThrowIfNull(boardViewController);
            ArgumentNullException.ThrowIfNull(cameraController);
            ArgumentNullException.ThrowIfNull(eventListener);

            _boardController = boardController;
            _boardViewController = boardViewController;
            _cameraController = cameraController;
            _eventListener = eventListener;
        }

        public void SetData([NotNull] BoardViewData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            Initialize(data.BoardId);
        }

        private void Initialize(string boardId)
        {
            // TODO: Check allow multiple Initialize. Add Clear ¿?

            InvalidOperationException.ThrowIfNull(_bottom);
            InvalidOperationException.ThrowIfNull(_boardController);
            InvalidOperationException.ThrowIfNull(_boardViewController);
            InvalidOperationException.ThrowIfNull(_cameraController);
            InvalidOperationException.ThrowIfNull(_eventListener);

            IReadonlyBoard board = _boardController.Initialize(boardId);

            _boardController.ResolveInstantiateInitialAndCascade();
            _boardViewController.Initialize(board);
            _cameraController.Initialize(board, _top.position.y, _bottom.position.y); // TODO: Needs to be done after InstantiateInitial so HighestNonEmptyRow is set. Consider adding OnHighestNonEmptyRowUpdated and CameraController sub
            _eventListener.Initialize();
        }
    }
}