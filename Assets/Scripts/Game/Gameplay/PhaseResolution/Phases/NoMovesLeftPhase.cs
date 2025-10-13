using Game.Gameplay.Moves;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public class NoMovesLeftPhase : Phase
    {
        [NotNull] private readonly IMovesContainer _movesContainer;

        public NoMovesLeftPhase([NotNull] IMovesContainer movesContainer)
        {
            ArgumentNullException.ThrowIfNull(movesContainer);

            _movesContainer = movesContainer;
        }

        protected override ResolveResult ResolveImpl(ResolveContext _)
        {
            IMoves moves = _movesContainer.Moves;

            InvalidOperationException.ThrowIfNull(moves);

            if (moves.Amount > 0)
            {
                return ResolveResult.NotUpdated;
            }

            // TODO: EventEnqueuer

            Debug.Log("YOU LOSE"); // TODO: Remove

            return ResolveResult.Stop;
        }
    }
}