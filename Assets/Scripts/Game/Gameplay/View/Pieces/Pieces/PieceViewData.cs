using System;
using Game.Gameplay.Pieces.Pieces;

namespace Game.Gameplay.View.Pieces.Pieces
{
    public class PieceViewData
    {
        public readonly IPiece Piece;
        public readonly Action OnReady;

        public PieceViewData(IPiece piece, Action onReady)
        {
            Piece = piece;
            OnReady = onReady;
        }
    }
}