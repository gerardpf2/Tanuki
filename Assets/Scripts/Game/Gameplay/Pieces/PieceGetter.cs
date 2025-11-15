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
                case PieceType.PlayerI:
                    piece = _pieceFactory.GetPlayerI();
                    break;
                case PieceType.PlayerO:
                    piece = _pieceFactory.GetPlayerO();
                    break;
                case PieceType.PlayerT:
                    piece = _pieceFactory.GetPlayerT();
                    break;
                case PieceType.PlayerJ:
                    piece = _pieceFactory.GetPlayerJ();
                    break;
                case PieceType.PlayerL:
                    piece = _pieceFactory.GetPlayerL();
                    break;
                case PieceType.PlayerS:
                    piece = _pieceFactory.GetPlayerS();
                    break;
                case PieceType.PlayerZ:
                    piece = _pieceFactory.GetPlayerZ();
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
                default:
                    ArgumentOutOfRangeException.Throw(pieceType);
                    return null;
            }

            InvalidOperationException.ThrowIfNot(piece.Type, ComparisonOperator.EqualTo, pieceType);

            return piece;
        }
    }
}