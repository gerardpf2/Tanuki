using Game.Gameplay.Camera;
using Game.Gameplay.Events;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Phases.Phases
{
    public class CameraTargetDesiredRowPhase : Phase
    {
        // TODO: Comment

        [NotNull] private readonly ICameraRowsUpdater _cameraRowsUpdater;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;

        public CameraTargetDesiredRowPhase(
            [NotNull] ICameraRowsUpdater cameraRowsUpdater,
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IEventFactory eventFactory)
        {
            ArgumentNullException.ThrowIfNull(cameraRowsUpdater);
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);

            _cameraRowsUpdater = cameraRowsUpdater;
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

            int rowOffset =
                _cameraRowsUpdater.TargetHighestNonEmptyRow() +
                _cameraRowsUpdater.TargetLockRow(resolveContext.PieceLockSourceCoordinate.Value.Row);

            if (rowOffset == 0)
            {
                return ResolveResult.NotUpdated;
            }

            _eventEnqueuer.Enqueue(_eventFactory.GetMoveCameraEvent(rowOffset));

            return ResolveResult.Updated;
        }
    }
}