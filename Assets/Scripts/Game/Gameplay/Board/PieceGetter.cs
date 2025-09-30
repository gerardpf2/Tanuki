using System.Collections.Generic;
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

        public IPiece Get(PieceType pieceType, IEnumerable<KeyValuePair<string, string>> state)
        {
            IPiece piece;

            switch (pieceType)
            {
                case PieceType.Test:
                {
                    piece = _pieceFactory.GetTest(state);

                    break;
                }
                case PieceType.PlayerBlock11:
                {
                    ThrowExceptionIfStateIsNotNull();

                    piece = _pieceFactory.GetPlayerBlock11();

                    break;
                }
                case PieceType.PlayerBlock12:
                {
                    ThrowExceptionIfStateIsNotNull();

                    piece = _pieceFactory.GetPlayerBlock12();

                    break;
                }
                case PieceType.PlayerBlock21:
                {
                    ThrowExceptionIfStateIsNotNull();

                    piece = _pieceFactory.GetPlayerBlock21();

                    break;
                }
                default:
                    ArgumentOutOfRangeException.Throw(pieceType);
                    return null;
            }

            InvalidOperationException.ThrowIfNot(piece.Type, ComparisonOperator.EqualTo, pieceType);

            return piece;

            void ThrowExceptionIfStateIsNotNull()
            {
                if (state is not null)
                {
                    ArgumentException.Throw($"Null expected. {pieceType} does not support it", nameof(state));
                }
            }
        }
    }
}