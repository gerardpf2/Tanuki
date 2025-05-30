using Game.Gameplay.Board;
using Game.Gameplay.EventEnqueueing;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public class GravityPhase : Phase, IGravityPhase
    {
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;

        private IBoard _board;

        public GravityPhase([NotNull] IEventEnqueuer eventEnqueuer, [NotNull] IEventFactory eventFactory) : base(-1, -1)
        {
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);

            _eventEnqueuer = eventEnqueuer;
            _eventFactory = eventFactory;
        }

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
            return false;
        }
    }
}