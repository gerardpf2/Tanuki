using Game.Gameplay.Camera;
using Game.Gameplay.Events;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Phases.Phases
{
    public class CameraTargetTopRowPhase : Phase
    {
        [NotNull] private readonly ICameraRowsUpdater _cameraRowsUpdater;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;

        public CameraTargetTopRowPhase(
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

        protected override ResolveResult ResolveImpl(ResolveContext _)
        {
            int rowOffset = _cameraRowsUpdater.TargetHighestNonEmptyRow();

            if (rowOffset == 0)
            {
                return ResolveResult.NotUpdated;
            }

            _eventEnqueuer.Enqueue(_eventFactory.GetMoveCameraEvent(rowOffset));

            return ResolveResult.Updated;
        }
    }
}