using Game.Gameplay.Moves;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.Phases.Phases
{
    public class NoMovesLeftPhase : Phase
    {
        [NotNull] private readonly IMoves _moves;

        public NoMovesLeftPhase([NotNull] IMoves moves)
        {
            ArgumentNullException.ThrowIfNull(moves);

            _moves = moves;
        }

        protected override ResolveResult ResolveImpl(ResolveContext _)
        {
            if (_moves.Amount > 0)
            {
                return ResolveResult.NotUpdated;
            }

            // TODO: EventEnqueuer

            Debug.Log("YOU LOSE"); // TODO: Remove

            return ResolveResult.Stop;
        }
    }
}