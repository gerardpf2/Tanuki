using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces
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

        public IPiece GetPlayerI()
        {
            return new PlayerI(_converter, GetNewPieceId());
        }

        public IPiece GetPlayerO()
        {
            return new PlayerO(_converter, GetNewPieceId());
        }

        public IPiece GetPlayerT()
        {
            return new PlayerT(_converter, GetNewPieceId());
        }

        public IPiece GetPlayerJ()
        {
            return new PlayerJ(_converter, GetNewPieceId());
        }

        public IPiece GetPlayerL()
        {
            return new PlayerL(_converter, GetNewPieceId());
        }

        public IPiece GetPlayerS()
        {
            return new PlayerS(_converter, GetNewPieceId());
        }

        public IPiece GetPlayerZ()
        {
            return new PlayerZ(_converter, GetNewPieceId());
        }

        public IPiece GetTest()
        {
            return new Test(_converter, GetNewPieceId());
        }

        private int GetNewPieceId()
        {
            return _pieceIdGetter.GetNew();
        }
    }
}