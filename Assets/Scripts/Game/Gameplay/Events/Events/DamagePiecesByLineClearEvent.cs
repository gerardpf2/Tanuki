using System.Collections.Generic;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Events.Events
{
    public class DamagePiecesByLineClearEvent : IEvent
    {
        [NotNull] private readonly IDictionary<int, IEnumerable<KeyValuePair<string, string>>> _pieceStatesById = new Dictionary<int, IEnumerable<KeyValuePair<string, string>>>();

        [NotNull]
        public IEnumerable<int> PieceIds => _pieceStatesById.Keys;

        public DamagePiecesByLineClearEvent([NotNull, ItemNotNull] IEnumerable<IPiece> pieces)
        {
            ArgumentNullException.ThrowIfNull(pieces);

            foreach (IPiece piece in pieces)
            {
                ArgumentNullException.ThrowIfNull(piece);

                _pieceStatesById[piece.Id] = piece.State;
            }
        }

        public IEnumerable<KeyValuePair<string, string>> GetState(int pieceId)
        {
            if (!_pieceStatesById.TryGetValue(pieceId, out IEnumerable<KeyValuePair<string, string>> pieceState))
            {
                InvalidOperationException.Throw($"State for piece with Id: {pieceId} cannot be found");
            }

            return pieceState;
        }
    }
}