using Game.Gameplay.Camera;
using Game.Gameplay.Events;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Phases.Phases
{
    public class CameraTargetPlayerPieceLockRowPhase : Phase
    {
        [NotNull] private readonly ICameraRowsUpdater _cameraRowsUpdater;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;

        protected override int? MaxResolveTimesPerIteration => 1;

        public CameraTargetPlayerPieceLockRowPhase(
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

            int lockRow = resolveContext.PieceLockSourceCoordinate.Value.Row;
            int rowOffset = _cameraRowsUpdater.TargetLockRow(lockRow);

            if (rowOffset != 0)
            {
                _eventEnqueuer.Enqueue(_eventFactory.GetMoveCameraEvent(rowOffset));
            }

            return ResolveResult.Updated;
        }
    }
}