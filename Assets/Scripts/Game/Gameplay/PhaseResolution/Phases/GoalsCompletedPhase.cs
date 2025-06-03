using Game.Gameplay.Goals;
using Game.Gameplay.Goals.Utils;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public class GoalsCompletedPhase : Phase, IGoalsCompletedPhase
    {
        [NotNull] private readonly IGoalsStateContainer _goalsStateContainer;

        public GoalsCompletedPhase([NotNull] IGoalsStateContainer goalsStateContainer) : base(-1, -1)
        {
            ArgumentNullException.ThrowIfNull(goalsStateContainer);

            _goalsStateContainer = goalsStateContainer;
        }

        public void Initialize()
        {
            // TODO: Remove if not needed

            Uninitialize();
        }

        public override void Uninitialize()
        {
            // TODO: Remove if not needed

            base.Uninitialize();
        }

        protected override ResolveResult ResolveImpl(ResolveContext _)
        {
            if (!_goalsStateContainer.AreAllCompleted())
            {
                return ResolveResult.NotUpdated;
            }

            // TODO: EventEnqueuer

            return ResolveResult.Stop;
        }
    }
}