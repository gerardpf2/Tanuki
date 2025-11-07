using System.Collections.Generic;
using Game.Gameplay.Phases.Phases;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Phases.Utils
{
    public static class PhaseResolverUtils
    {
        public static void Resolve(
            [NotNull] this IPhaseResolver phaseResolver,
            IPhase phase,
            ResolveContext resolveContext)
        {
            ArgumentNullException.ThrowIfNull(phaseResolver);

            IReadOnlyList<IPhase> phases = new [] { phase };

            phaseResolver.Resolve(phases, resolveContext);
        }
    }
}