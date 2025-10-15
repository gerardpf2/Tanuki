using Game.Gameplay.Board.Pieces;
using Infrastructure.System;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Board
{
    public class PieceFactory : IPieceFactory
    {
        [NotNull] private readonly IPieceIdGetter _pieceIdGetter;
        [NotNull] private readonly IConverter _converter;

        public PieceFactory([NotNull] IPieceIdGetter pieceIdGetter, [NotNull] IConverter converter)
        {
            ArgumentNullException.ThrowIfNull(pieceIdGetter);
            ArgumentNullException.ThrowIfNull(converter);

            _pieceIdGetter = pieceIdGetter;
            _converter = converter;
        }

        public IPiece GetTest()
        {
            return new Test(_converter, GetNewPieceId());
        }

        public IPiece GetPlayerBlock11()
        {
            return new PlayerBlock11(_converter, GetNewPieceId());
        }

        public IPiece GetPlayerBlock21()
        {
            return new PlayerBlock21(_converter, GetNewPieceId());
        }

        private int GetNewPieceId()
        {
            return _pieceIdGetter.GetNew();
        }
    }
}