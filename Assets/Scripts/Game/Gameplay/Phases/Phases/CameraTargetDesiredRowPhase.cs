using System;
using Game.Gameplay.Board;
using Game.Gameplay.Camera;
using Game.Gameplay.Events;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.Phases.Phases
{
    public class CameraTargetDesiredRowPhase : Phase
    {
        // Targets the highest board row that allows the player piece ghost to be fully visible

        [NotNull] private readonly IBoardContainer _boardContainer;
        [NotNull] private readonly ICamera _camera;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;

        public CameraTargetDesiredRowPhase(
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

        protected override ResolveResult ResolveImpl([NotNull] ResolveContext resolveContext)
        {
            ArgumentNullException.ThrowIfNull(resolveContext);

            if (!resolveContext.PieceLockSourceCoordinate.HasValue)
            {
                return ResolveResult.NotUpdated;
            }

            int prevCameraBottomRow = _camera.BottomRow;

            TargetBoardHighestNonEmptyRow();
            TargetPlayerPieceLockRow(resolveContext.PieceLockSourceCoordinate.Value.Row);

            int newCameraBottomRow = _camera.BottomRow;

            int rowOffset = newCameraBottomRow - prevCameraBottomRow;

            if (rowOffset == 0)
            {
                return ResolveResult.NotUpdated;
            }

            _eventEnqueuer.Enqueue(_eventFactory.GetMoveCameraEvent(rowOffset));

            return ResolveResult.Updated;
        }

        private void TargetBoardHighestNonEmptyRow()
        {
            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            _camera.TopRow = Math.Max(board.HighestNonEmptyRow + _camera.ExtraRowsOnTop, _camera.VisibleRows - 1);
        }

        private void TargetPlayerPieceLockRow(int playerPieceLockRow)
        {
            _camera.BottomRow = Math.Min(_camera.BottomRow, playerPieceLockRow);
        }
    }
}