using Game.Gameplay.Goals;
using Game.Gameplay.Goals.Utils;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public class GoalsCompletedPhase : Phase
    {
        [NotNull] private readonly IGoalsContainer _goalsContainer;

        public GoalsCompletedPhase([NotNull] IGoalsContainer goalsContainer) : base(-1, -1)
        {
            ArgumentNullException.ThrowIfNull(goalsContainer);

            _goalsContainer = goalsContainer;
        }

        protected override ResolveResult ResolveImpl(ResolveContext _)
        {
            if (!_goalsContainer.AreAllCompleted())
            {
                return ResolveResult.NotUpdated;
            }

            // TODO: EventEnqueuer

            return ResolveResult.Stop;
        }
    }
}