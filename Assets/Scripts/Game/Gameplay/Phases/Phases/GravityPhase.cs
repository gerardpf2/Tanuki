using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.Events;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Phases.Phases
{
    public class GravityPhase : Phase
    {
        [NotNull] private readonly IBoard _board;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;

        public GravityPhase([NotNull] IBoard board, [NotNull] IEventEnqueuer eventEnqueuer)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(eventEnqueuer);

            _board = board;
            _eventEnqueuer = eventEnqueuer;
        }

        protected override ResolveResult ResolveImpl(ResolveContext _)
        {
            IDictionary<int, int> fallData = new Dictionary<int, int>();

            while (TryMovePieces(fallData));

            if (fallData.Count <= 0)
            {
                return ResolveResult.NotUpdated;
            }

            MovePiecesByGravityEvent movePiecesByGravityEvent = new(fallData);

            _eventEnqueuer.Enqueue(movePiecesByGravityEvent);

            return ResolveResult.Updated;
        }

        private bool TryMovePieces([NotNull] IDictionary<int, int> fallData)
        {
            ArgumentNullException.ThrowIfNull(fallData);

            bool resolved = false;

            foreach (int pieceId in _board.GetDistinctPieceIdsSortedByRowThenByColumn())
            {
                if (TryMovePiece(fallData, pieceId))
                {
                    resolved = true;
                }
            }

            return resolved;
        }

        private bool TryMovePiece([NotNull] IDictionary<int, int> fallData, int pieceId)
        {
            ArgumentNullException.ThrowIfNull(fallData);

            IPiece piece = _board.GetPiece(pieceId);

            if (!piece.AffectedByGravity)
            {
                return false;
            }

            Coordinate sourceCoordinate = _board.GetSourceCoordinate(pieceId);

            int fall = _board.ComputePieceFall(piece, sourceCoordinate);

            if (fall <= 0)
            {
                return false;
            }

            UpdateFallData();
            UpdateBoard();

            return true;

            void UpdateFallData()
            {
                if (fallData.TryGetValue(pieceId, out int currentFall))
                {
                    fallData[pieceId] = currentFall + fall;
                }
                else
                {
                    fallData.Add(pieceId, fall);
                }
            }

            void UpdateBoard()
            {
                int rowOffset = -fall;
                const int columnOffset = 0;

                _board.MovePiece(pieceId, rowOffset, columnOffset);
            }
        }
    }
}