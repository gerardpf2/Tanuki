using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Model.Board.Utils
{
    public static class BoardUtils
    {
        public static bool IsInside([NotNull] this IBoard board, Coordinate coordinate)
        {
            ArgumentNullException.ThrowIfNull(board);

            return
                coordinate.Row >= 0 && coordinate.Row < board.Rows &&
                coordinate.Column >= 0 && coordinate.Column < board.Columns;
        }
    }
}