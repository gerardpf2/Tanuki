using Game.Gameplay.Board;
using Game.Gameplay.View.Camera;
using Game.Gameplay.View.EventResolution;
using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Board
{
    public class BoardViewModel : ViewModel, IDataSettable<BoardViewData>
    {
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

            InvalidOperationException.ThrowIfNull(_boardController);
            InvalidOperationException.ThrowIfNull(_boardViewController);
            InvalidOperationException.ThrowIfNull(_cameraController);
            InvalidOperationException.ThrowIfNull(_eventListener);

            _boardController.Initialize(boardId);

            int rows = _boardController.Rows;
            int columns = _boardController.Columns;

            _boardViewController.Initialize(rows, columns);
            _cameraController.Initialize(rows, columns);
            _eventListener.Initialize();

            _boardController.ResolveInstantiateInitialAndCascade();
        }
    }
}