using System;
using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.Camera;
using Game.Gameplay.Events;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.Phases.Phases
{
    public class LineClearPhase : Phase
    {
        [NotNull] private readonly IBoardContainer _boardContainer;
        [NotNull] private readonly ICamera _camera;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;

        public LineClearPhase(
            [NotNull] IBoardContainer boardContainer,
            [NotNull] ICamera camera,
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IEventFactory eventFactory)
        {
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(camera);
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);

            _boardContainer = boardContainer;
            _camera = camera;
            _eventEnqueuer = eventEnqueuer;
            _eventFactory = eventFactory;
        }

        protected override ResolveResult ResolveImpl(ResolveContext _)
        {
            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            bool resolved = false;

            int bottomRow = _camera.BottomRow;
            int topRow = Math.Min(board.HighestNonEmptyRow, _camera.TopRow);

            for (int row = bottomRow; row <= topRow; ++row)
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