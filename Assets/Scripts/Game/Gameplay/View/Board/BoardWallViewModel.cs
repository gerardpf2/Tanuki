using Game.Gameplay.Board;
using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Board
{
    public class BoardWallViewModel : ViewModel
    {
        // TODO: Use bindings

        private IBoard _board;

        protected override void Awake()
        {
            base.Awake();

            InjectResolver.Resolve(this);
        }

        public void Inject([NotNull] IBoard board)
        {
            ArgumentNullException.ThrowIfNull(board);

            _board = board;

            UpdateWidth();
        }

        private void UpdateWidth()
        {
            InvalidOperationException.ThrowIfNull(_board);

            // TODO
        }
    }
}