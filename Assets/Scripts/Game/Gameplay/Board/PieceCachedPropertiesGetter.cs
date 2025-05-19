using System.Collections.Generic;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.Board.Pieces.Utils;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Board
{
    public class PieceCachedPropertiesGetter : IPieceCachedPropertiesGetter
    {
        private sealed class PieceProperties
        {
            public readonly int TopMostRowOffset;
            public readonly int RightMostColumnOffset;

            public PieceProperties(int topMostRowOffset, int rightMostColumnOffset)
            {
                TopMostRowOffset = topMostRowOffset;
                RightMostColumnOffset = rightMostColumnOffset;
            }
        }

        [NotNull] private readonly IDictionary<PieceType, PieceProperties> _pieceProperties = new Dictionary<PieceType, PieceProperties>();

        public int GetTopMostRowOffset([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            return GetPieceProperties(piece).TopMostRowOffset;
        }

        public int GetRightMostColumnOffset([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            return GetPieceProperties(piece).RightMostColumnOffset;
        }

        [NotNull]
        private PieceProperties GetPieceProperties([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            CachePiecePropertiesIfNeeded(piece);

            if (!_pieceProperties.TryGetValue(piece.Type, out PieceProperties pieceProperties))
            {
                InvalidOperationException.Throw(); // TODO
            }

            InvalidOperationException.ThrowIfNull(pieceProperties);

            return pieceProperties;
        }

        private void CachePiecePropertiesIfNeeded([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            if (_pieceProperties.ContainsKey(piece.Type))
            {
                return;
            }

            CachePieceProperties(piece);
        }

        private void CachePieceProperties([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            piece.GetTopMostRowAndRightMostColumnOffsets(out int topMostRowOffset, out int rightMostColumnOffset);

            PieceProperties pieceProperties = new(topMostRowOffset, rightMostColumnOffset);

            if (!_pieceProperties.TryAdd(piece.Type, pieceProperties))
            {
                InvalidOperationException.Throw(); // TODO
            }
        }
    }
}