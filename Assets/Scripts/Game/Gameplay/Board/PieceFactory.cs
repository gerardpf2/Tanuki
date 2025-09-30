using System.Collections.Generic;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.Board.Pieces.Utils;
using Infrastructure.System;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Board
{
    public class PieceFactory : IPieceFactory
    {
        [NotNull] private readonly IConverter _converter;

        private uint _id;

        public PieceFactory([NotNull] IConverter converter)
        {
            ArgumentNullException.ThrowIfNull(converter);

            _converter = converter;
        }

        public IPiece GetTest(IEnumerable<KeyValuePair<string, string>> customData)
        {
            return new Test(_converter, GetNewId()).WithCustomData(customData);
        }

        public IPiece GetPlayerBlock11()
        {
            return new PlayerBlock11(_converter, GetNewId());
        }

        public IPiece GetPlayerBlock12()
        {
            return new PlayerBlock12(_converter, GetNewId());
        }

        public IPiece GetPlayerBlock21()
        {
            return new PlayerBlock21(_converter, GetNewId());
        }

        private uint GetNewId()
        {
            return _id++;
        }
    }
}