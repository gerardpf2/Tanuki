using Game.Gameplay.Goals;
using Game.Gameplay.Goals.Utils;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.Phases.Phases
{
    public class GoalsCompletedPhase : Phase
    {
        [NotNull] private readonly IGoals _goals;

        public GoalsCompletedPhase([NotNull] IGoals goals)
        {
            ArgumentNullException.ThrowIfNull(goals);

            _goals = goals;
        }

        protected override ResolveResult ResolveImpl(ResolveContext _)
        {
            if (!_goals.AreCompleted())
            {
                return ResolveResult.NotUpdated;
            }

            // TODO: EventEnqueuer

            Debug.Log("YOU WIN"); // TODO: Remove

            return ResolveResult.Stop;
        }
    }
}