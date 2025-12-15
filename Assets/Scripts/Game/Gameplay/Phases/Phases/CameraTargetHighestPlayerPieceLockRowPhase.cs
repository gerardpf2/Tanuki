using Game.Gameplay.Board;
using Game.Gameplay.Camera;
using Game.Gameplay.Events;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Events.Reasons;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Phases.Phases
{
    public class CameraTargetHighestPlayerPieceLockRowPhase : Phase
    {
        // Targets the highest board row that allows the player piece ghost to be fully visible

        [NotNull] private readonly IMoveCameraHelper _moveCameraHelper;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;

        public CameraTargetHighestPlayerPieceLockRowPhase(
            [NotNull] IMoveCameraHelper moveCameraHelper,
            [NotNull] IEventEnqueuer eventEnqueuer)
        {
            ArgumentNullException.ThrowIfNull(moveCameraHelper);
            ArgumentNullException.ThrowIfNull(eventEnqueuer);

            _moveCameraHelper = moveCameraHelper;
            _eventEnqueuer = eventEnqueuer;
        }

        protected override ResolveResult ResolveImpl([NotNull] ResolveContext resolveContext)
        {
            ArgumentNullException.ThrowIfNull(resolveContext);

            if (!resolveContext.PieceLockSourceCoordinate.HasValue)
            {
                return ResolveResult.NotUpdated;
            }

            Coordinate pieceLockSourceCoordinate = resolveContext.PieceLockSourceCoordinate.Value;

            MoveCameraEvent moveCameraEvent =
                _moveCameraHelper.TargetHighestPlayerPieceLockRow(
                    pieceLockSourceCoordinate.Row,
                    MoveCameraReason.Regular
                );

            if (moveCameraEvent.RowOffset == 0)
            {
                return ResolveResult.NotUpdated;
            }

            _eventEnqueuer.Enqueue(moveCameraEvent);

            return ResolveResult.Updated;
        }
    }
}