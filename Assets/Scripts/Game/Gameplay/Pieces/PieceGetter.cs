using Game.Common.Pieces;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;

namespace Game.Gameplay.Pieces
{
    public class PieceGetter : IPieceGetter
    {
        [NotNull] private readonly IPieceFactory _pieceFactory;

        public PieceGetter([NotNull] IPieceFactory pieceFactory)
        {
            ArgumentNullException.ThrowIfNull(pieceFactory);

            _pieceFactory = pieceFactory;
        }

        public IPiece Get(PieceType pieceType)
        {
            IPiece piece;

            switch (pieceType)
            {
                case PieceType.I:
                    piece = _pieceFactory.GetI();
                    break;
                case PieceType.O:
                    piece = _pieceFactory.GetO();
                    break;
                case PieceType.T:
                    piece = _pieceFactory.GetT();
                    break;
                case PieceType.J:
                    piece = _pieceFactory.GetJ();
                    break;
                case PieceType.L:
                    piece = _pieceFactory.GetL();
                    break;
                case PieceType.S:
                    piece = _pieceFactory.GetS();
                    break;
                case PieceType.Z:
                    piece = _pieceFactory.GetZ();
                    break;
                case PieceType.BlockI:
                    piece = _pieceFactory.GetBlockI();
                    break;
                case PieceType.BlockO:
                    piece = _pieceFactory.GetBlockO();
                    break;
                case PieceType.BlockT:
                    piece = _pieceFactory.GetBlockT();
                    break;
                case PieceType.BlockJ:
                    piece = _pieceFactory.GetBlockJ();
                    break;
                case PieceType.BlockL:
                    piece = _pieceFactory.GetBlockL();
                    break;
                case PieceType.BlockS:
                    piece = _pieceFactory.GetBlockS();
                    break;
                case PieceType.BlockZ:
                    piece = _pieceFactory.GetBlockZ();
                    break;
                case PieceType.Test:
                    piece = _pieceFactory.GetTest();
                    break;
                case PieceType.StaticBlock:
                    piece = _pieceFactory.GetStaticBlock();
                    break;
                case PieceType.Tato:
                    piece = _pieceFactory.GetTato();
                    break;
                default:
                    ArgumentOutOfRangeException.Throw(pieceType);
                    return null;
            }

            InvalidOperationException.ThrowIfNot(piece.Type, ComparisonOperator.EqualTo, pieceType);

            return piece;
        }
    }
}