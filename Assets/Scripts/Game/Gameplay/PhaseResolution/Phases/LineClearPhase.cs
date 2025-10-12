using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.EventEnqueueing;
using Game.Gameplay.EventEnqueueing.Events.Reasons;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public class LineClearPhase : Phase
    {
        [NotNull] private readonly IBoardContainer _boardContainer;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;

        public LineClearPhase(
            [NotNull] IBoardContainer boardContainer,
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IEventFactory eventFactory)
        {
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);

            _boardContainer = boardContainer;
            _eventEnqueuer = eventEnqueuer;
            _eventFactory = eventFactory;
        }

        protected override ResolveResult ResolveImpl(ResolveContext _)
        {
            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            bool resolved = false;

            int rows = board.Rows;

            for (int row = 0; row < rows; ++row)
            {
                resolved = TryDamageRow(row) || resolved;
            }

            return resolved ? ResolveResult.Updated : ResolveResult.NotUpdated;
        }

        private bool TryDamageRow(int row)
        {
            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            IReadOnlyCollection<KeyValuePair<int, int>> pieceIdsInRow = new List<KeyValuePair<int, int>>(board.GetPieceIdsInRow(row));

            if (pieceIdsInRow.Count < board.Columns)
            {
                return false;
            }

            bool anyDamaged = false;

            foreach ((int pieceId, int column) in pieceIdsInRow)
            {
                IPiece piece = board.GetPiece(pieceId);

                board.GetPieceRowColumnOffset(pieceId, row, column, out int rowOffset, out int columnOffset);

                piece.Damage(rowOffset, columnOffset);

                _eventEnqueuer.Enqueue(_eventFactory.GetDamagePieceEvent(piece, DamagePieceReason.LineClear));

                anyDamaged = true;
            }

            return anyDamaged;
        }
    }
}