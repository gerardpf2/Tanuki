using System;
using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.Camera;
using Game.Gameplay.Events;
using Game.Gameplay.Pieces.Pieces;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.Phases.Phases
{
    public class LineClearPhase : Phase
    {
        [NotNull] private readonly IBoard _board;
        [NotNull] private readonly ICamera _camera;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;

        public LineClearPhase(
            [NotNull] IBoard board,
            [NotNull] ICamera camera,
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IEventFactory eventFactory)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(camera);
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);

            _board = board;
            _camera = camera;
            _eventEnqueuer = eventEnqueuer;
            _eventFactory = eventFactory;
        }

        protected override ResolveResult ResolveImpl(ResolveContext _)
        {
            ICollection<IPiece> damagedPieces = new HashSet<IPiece>();

            int bottomRow = _camera.BottomRow;
            int topRow = Math.Min(_board.HighestNonEmptyRow, _camera.TopRow);

            for (int row = bottomRow; row <= topRow; ++row)
            {
                TryDamageRow(row, damagedPieces);
            }

            if (damagedPieces.Count <= 0)
            {
                return ResolveResult.NotUpdated;
            }

            _eventEnqueuer.Enqueue(_eventFactory.GetDamagePiecesByLineClearEvent(damagedPieces));

            return ResolveResult.Updated;
        }

        private void TryDamageRow(int row, [NotNull] ICollection<IPiece> damagedPieces)
        {
            ArgumentNullException.ThrowIfNull(damagedPieces);

            IReadOnlyCollection<KeyValuePair<int, int>> pieceIdsInRow = new List<KeyValuePair<int, int>>(_board.GetPieceIdsInRow(row));

            if (pieceIdsInRow.Count < _board.Columns)
            {
                return;
            }

            foreach ((int pieceId, int column) in pieceIdsInRow)
            {
                IPiece piece = _board.GetPiece(pieceId);

                _board.GetPieceRowColumnOffset(pieceId, row, column, out int rowOffset, out int columnOffset);

                piece.Damage(rowOffset, columnOffset);

                damagedPieces.Add(piece);
            }
        }
    }
}