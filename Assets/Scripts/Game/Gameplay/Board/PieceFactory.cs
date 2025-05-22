using System.Collections.Generic;
using Game.Gameplay.Board.Pieces;
using Infrastructure.System;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Board
{
    public class PieceFactory : IPieceFactory
    {
        [NotNull] private readonly IConverter _converter;

        public PieceFactory([NotNull] IConverter converter)
        {
            ArgumentNullException.ThrowIfNull(converter);

            _converter = converter;
        }

        public IPiece GetTest(IEnumerable<KeyValuePair<string, string>> customData)
        {
            return ProcessCustomData(new Test(_converter), customData);
        }

        public IPiece GetPlayerBlock11()
        {
            return new PlayerBlock11();
        }

        public IPiece GetPlayerBlock12()
        {
            return new PlayerBlock12();
        }

        public IPiece GetPlayerBlock21()
        {
            return new PlayerBlock21();
        }

        private static IPiece ProcessCustomData(IPiece piece, IEnumerable<KeyValuePair<string, string>> customData)
        {
            if (piece is IPieceUpdater pieceUpdater)
            {
                pieceUpdater.ProcessCustomData(customData);
            }

            return piece;
        }
    }
}