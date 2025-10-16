using Game.Gameplay.Board.Pieces;
using Infrastructure.System;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;

namespace Game.Gameplay.Board
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
                case PieceType.Test:
                {
                    piece = _pieceFactory.GetTest();

                    break;
                }
                case PieceType.PlayerO:
                {
                    piece = _pieceFactory.GetPlayerO();

                    break;
                }
                case PieceType.PlayerI:
                {
                    piece = _pieceFactory.GetPlayerI();

                    break;
                }
                default:
                    ArgumentOutOfRangeException.Throw(pieceType);
                    return null;
            }

            InvalidOperationException.ThrowIfNot(piece.Type, ComparisonOperator.EqualTo, pieceType);

            return piece;
        }
    }
}