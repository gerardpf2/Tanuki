using System;
using Game.Gameplay.Board;
using Game.Gameplay.Camera;
using Game.Gameplay.Events;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.Phases.Phases
{
    public class CameraTargetPlayerPieceLockRowPhase : Phase
    {
        private const int ExtraRowsOnBottom = 3;

        [NotNull] private readonly ICamera _camera;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;

        protected override int? MaxResolveTimesPerIteration => 1;

        public CameraTargetPlayerPieceLockRowPhase(
            [NotNull] ICamera camera,
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IEventFactory eventFactory)
        {
            ArgumentNullException.ThrowIfNull(camera);
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);

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

            Coordinate lockSourceCoordinate = resolveContext.PieceLockSourceCoordinate.Value;

            int prevBottomRow = _camera.BottomRow;
            int newBottomRow = lockSourceCoordinate.Row;

            if (prevBottomRow <= newBottomRow)
            {
                return ResolveResult.NotUpdated;
            }

            newBottomRow = Math.Max(newBottomRow - ExtraRowsOnBottom, 0);

            _camera.BottomRow = newBottomRow;

            int rowOffset = newBottomRow - prevBottomRow;

            _eventEnqueuer.Enqueue(_eventFactory.GetMoveCameraEvent(rowOffset));

            return ResolveResult.Updated;
        }
    }
}