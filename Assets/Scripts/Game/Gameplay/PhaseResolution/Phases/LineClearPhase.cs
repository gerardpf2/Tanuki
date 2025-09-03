using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.EventEnqueueing;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public class LineClearPhase : Phase, ILineClearPhase
    {
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;

        private IReadonlyBoard _board;

        public LineClearPhase([NotNull] IEventEnqueuer eventEnqueuer, [NotNull] IEventFactory eventFactory) : base(-1, -1)
        {
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);

            _eventEnqueuer = eventEnqueuer;
            _eventFactory = eventFactory;
        }

        public void Initialize([NotNull] IReadonlyBoard board)
        {
            ArgumentNullException.ThrowIfNull(board);

            Uninitialize();

            _board = board;
        }

        public override void Uninitialize()
        {
            base.Uninitialize();

            _board = null;
        }

        protected override bool ResolveImpl(ResolveContext _)
        {
            InvalidOperationException.ThrowIfNull(_board);

            bool resolved = false;

            int rows = _board.Rows;

            for (int row = 0; row < rows; ++row)
            {
                resolved = TryDamageRow(row) || resolved;
            }

            return resolved;
        }

        private bool TryDamageRow(int row)
        {
            InvalidOperationException.ThrowIfNull(_board);

            ICollection<PiecePlacement> rowPieces = _board.GetPiecesInRow(row);

            int columns = _board.Columns;

            if (rowPieces.Count != columns)
            {
                return false;
            }

            bool anyDamaged = false;

            foreach (PiecePlacement piecePlacement in rowPieces)
            {
                IPiece piece = piecePlacement.Piece;

                if (piece is not IPieceUpdater pieceUpdater)
                {
                    continue;
                }

                _board.GetPieceRowColumnOffset(
                    piece,
                    piecePlacement.Row,
                    piecePlacement.Column,
                    out int rowOffset,
                    out int columnOffset
                );

                pieceUpdater.Damage(rowOffset, columnOffset);

                _eventEnqueuer.Enqueue(_eventFactory.GetDamagePieceEvent(piece));

                anyDamaged = true;
            }

            return anyDamaged;
        }
    }
}