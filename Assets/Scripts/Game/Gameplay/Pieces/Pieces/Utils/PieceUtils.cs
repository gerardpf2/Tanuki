using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Utils;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces.Utils
{
    public static class PieceUtils
    {
        [NotNull]
        public static IEnumerable<Coordinate> GetCoordinates([NotNull] this IPiece piece, Coordinate sourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(piece);

            int height = piece.Height;
            int width = piece.Width;

            for (int rowOffset = 0; rowOffset < height; ++rowOffset)
            {
                for (int columnOffset = 0; columnOffset < width; ++columnOffset)
                {
                    if (piece.IsFilled(rowOffset, columnOffset))
                    {
                        yield return sourceCoordinate.WithOffset(rowOffset, columnOffset);
                    }
                }
            }
        }

        [NotNull]
        public static IPiece WithState([NotNull] this IPiece piece, IEnumerable<KeyValuePair<string, string>> state)
        {
            ArgumentNullException.ThrowIfNull(piece);

            piece.ProcessState(state);

            return piece;
        }
    }
}