using Game.Gameplay.Board;
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
        private IEventListener _eventListener;

        protected override void Awake()
        {
            base.Awake();

            InjectResolver.Resolve(this);
        }

        public void Inject(
            [NotNull] IBoardController boardController,
            [NotNull] IBoardViewController boardViewController,
            [NotNull] IEventListener eventListener)
        {
            ArgumentNullException.ThrowIfNull(boardController);
            ArgumentNullException.ThrowIfNull(boardViewController);
            ArgumentNullException.ThrowIfNull(eventListener);

            _boardController = boardController;
            _boardViewController = boardViewController;
            _eventListener = eventListener;
        }

        public void SetData([NotNull] BoardViewData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            Initialize(data.BoardId);
        }

        private void Initialize(string boardId)
        {
            InvalidOperationException.ThrowIfNull(_boardController);
            InvalidOperationException.ThrowIfNull(_boardViewController);
            InvalidOperationException.ThrowIfNull(_eventListener);

            _boardController.Initialize(boardId);
            _boardViewController.Initialize(_boardController.Rows, _boardController.Columns);
            _eventListener.Initialize();

            _boardController.ResolveInstantiateInitialAndCascade();
        }
    }
}