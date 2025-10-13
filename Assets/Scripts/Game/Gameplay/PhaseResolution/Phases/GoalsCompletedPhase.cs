using Game.Gameplay.Goals;
using Game.Gameplay.Goals.Utils;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public class GoalsCompletedPhase : Phase
    {
        [NotNull] private readonly IGoalsContainer _goalsContainer;

        public GoalsCompletedPhase([NotNull] IGoalsContainer goalsContainer)
        {
            ArgumentNullException.ThrowIfNull(goalsContainer);

            _goalsContainer = goalsContainer;
        }

        protected override ResolveResult ResolveImpl(ResolveContext _)
        {
            IGoals goals = _goalsContainer.Goals;

            InvalidOperationException.ThrowIfNull(goals);

            if (!goals.AreCompleted())
            {
                return ResolveResult.NotUpdated;
            }

            // TODO: EventEnqueuer

            Debug.Log("YOU WIN"); // TODO: Remove

            return ResolveResult.Stop;
        }
    }
}