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

            foreach ((int rowOffset, int columnOffset) in piece.GetFilledOffsets())
            {
                yield return sourceCoordinate.WithOffset(rowOffset, columnOffset);
            }
        }

        [NotNull]
        public static IEnumerable<Coordinate> GetUndamagedCoordinates(
            [NotNull] this IPiece piece,
            Coordinate sourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(piece);

            foreach ((int rowOffset, int columnOffset) in piece.GetFilledOffsets())
            {
                if (!piece.IsDamaged(rowOffset, columnOffset))
                {
                    yield return sourceCoordinate.WithOffset(rowOffset, columnOffset);
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

        public static void ResetRotation([NotNull] this IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            if (piece.CanRotate)
            {
                piece.Rotation = 0;
            }
        }

        [NotNull]
        private static IEnumerable<(int, int)> GetFilledOffsets([NotNull] this IPiece piece)
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
                        yield return (rowOffset, columnOffset);
                    }
                }
            }
        }
    }
}