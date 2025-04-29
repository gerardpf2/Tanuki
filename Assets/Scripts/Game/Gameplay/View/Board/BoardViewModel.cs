using Game.Gameplay.Board;
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

        protected override void Awake()
        {
            base.Awake();

            InjectResolver.Resolve(this);
        }

        public void Inject(
            [NotNull] IBoardController boardController,
            [NotNull] IBoardViewController boardViewController)
        {
            ArgumentNullException.ThrowIfNull(boardController);
            ArgumentNullException.ThrowIfNull(boardViewController);

            _boardController = boardController;
            _boardViewController = boardViewController;
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

            _boardController.Initialize(boardId);
            _boardViewController.Initialize(_boardController.Rows, _boardController.Columns);

            _boardController.ResolveInstantiateInitialPhase();
        }
    }
}