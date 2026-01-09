using System.Collections.Generic;
using Game.Gameplay.Phases.Phases;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Phases
{
    public class PhaseContainer : IPhaseContainer
    {
        [NotNull] private readonly IPhaseResolver _phaseResolver;
        private readonly IReadOnlyList<IPhase> _phases;

        public PhaseContainer([NotNull] IPhaseResolver phaseResolver, params IPhase[] phases)
        {
            ArgumentNullException.ThrowIfNull(phaseResolver);

            _phaseResolver = phaseResolver;
            _phases = phases;
        }

        public void Resolve(ResolveContext resolveContext)
        {
            _phaseResolver.Resolve(_phases, resolveContext);
        }
    }
}