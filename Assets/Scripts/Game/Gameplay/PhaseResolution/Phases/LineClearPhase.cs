using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.Board.Utils;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public class LineClearPhase : Phase, ILineClearPhase
    {
        private IBoard _board;

        public LineClearPhase() : base(-1, -1) { }

        public void Initialize([NotNull] IBoard board)
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

            IReadOnlyCollection<IPiece> rowPieces = _board.GetRowPieces(row);

            int columns = _board.Columns;

            if (rowPieces.Count != columns)
            {
                return false;
            }

            bool anyDamaged = false;

            foreach (IPiece piece in rowPieces)
            {
                if (piece is not IPieceUpdater pieceUpdater)
                {
                    continue;
                }

                pieceUpdater.Damage(0, 0); // TODO

                // TODO: EventEnqueuer

                anyDamaged = true;
            }

            return anyDamaged;
        }
    }
}