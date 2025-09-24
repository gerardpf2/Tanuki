using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Board
{
    public class BoardContainer : IBoardContainer
    {
        public IBoard Board { get; private set; }

        public void Initialize([NotNull] IBoard board)
        {
            ArgumentNullException.ThrowIfNull(board);

            Uninitialize();

            Board = board;
        }

        public void Uninitialize()
        {
            Board = null;
        }
    }
}