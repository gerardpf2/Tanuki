using System.Collections.Generic;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.EventEnqueueing.Events
{
    public class MovePiecesByGravityEvent : IEvent
    {
        public class PieceMovementData
        {
            public readonly int PieceId;
            public readonly int RowOffset;
            public readonly int ColumnOffset;

            public PieceMovementData(int pieceId, int rowOffset, int columnOffset)
            {
                PieceId = pieceId;
                RowOffset = rowOffset;
                ColumnOffset = columnOffset;
            }
        }

        [NotNull, ItemNotNull] private readonly Queue<PieceMovementData> _piecesMovementsData = new(); // ItemNotNull as long as all Add check for null

        [NotNull, ItemNotNull]
        public IEnumerable<PieceMovementData> PiecesMovementsData => _piecesMovementsData;

        public void Add([NotNull] PieceMovementData pieceMovementData)
        {
            ArgumentNullException.ThrowIfNull(pieceMovementData);

            _piecesMovementsData.Enqueue(pieceMovementData);
        }
    }
}