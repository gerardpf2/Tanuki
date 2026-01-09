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

        public IPiece GetI()
        {
            return new I(_converter, GetNewPieceId());
        }

        public IPiece GetO()
        {
            return new O(_converter, GetNewPieceId());
        }

        public IPiece GetT()
        {
            return new T(_converter, GetNewPieceId());
        }

        public IPiece GetJ()
        {
            return new J(_converter, GetNewPieceId());
        }

        public IPiece GetL()
        {
            return new L(_converter, GetNewPieceId());
        }

        public IPiece GetS()
        {
            return new S(_converter, GetNewPieceId());
        }

        public IPiece GetZ()
        {
            return new Z(_converter, GetNewPieceId());
        }

        public IPiece GetBlockI()
        {
            return new BlockI(_converter, GetNewPieceId());
        }

        public IPiece GetBlockO()
        {
            return new BlockO(_converter, GetNewPieceId());
        }

        public IPiece GetBlockT()
        {
            return new BlockT(_converter, GetNewPieceId());
        }

        public IPiece GetBlockJ()
        {
            return new BlockJ(_converter, GetNewPieceId());
        }

        public IPiece GetBlockL()
        {
            return new BlockL(_converter, GetNewPieceId());
        }

        public IPiece GetBlockS()
        {
            return new BlockS(_converter, GetNewPieceId());
        }

        public IPiece GetBlockZ()
        {
            return new BlockZ(_converter, GetNewPieceId());
        }

        public IPiece GetStaticBlock()
        {
            return new StaticBlock(_converter, GetNewPieceId());
        }

        public IPiece GetTato()
        {
            return new Tato(_converter, GetNewPieceId());
        }

        public IPiece GetTata()
        {
            return new Tata(_converter, GetNewPieceId());
        }

        public IPiece GetTete()
        {
            return new Tete(_converter, GetNewPieceId());
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