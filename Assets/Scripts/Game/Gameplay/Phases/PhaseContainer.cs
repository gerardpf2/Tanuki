using System.Collections.Generic;
using Game.Common;
using Game.Gameplay.Phases.Phases;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Phases
{
    public class PhaseContainer : IPhaseContainer
    {
        [NotNull] private readonly IPhaseResolver _phaseResolver;
        [NotNull, ItemNotNull] private readonly IReadOnlyList<IPhase> _phases;

        private InitializedLabel _initializedLabel;

        public PhaseContainer([NotNull] IPhaseResolver phaseResolver, [NotNull, ItemNotNull] params IPhase[] phases)
        {
            ArgumentNullException.ThrowIfNull(phaseResolver);
            ArgumentNullException.ThrowIfNull(phases);

            List<IPhase> phasesCopy = new();

            foreach (IPhase phase in phases)
            {
                ArgumentNullException.ThrowIfNull(phase);

                phasesCopy.Add(phase);
            }

            _phaseResolver = phaseResolver;
            _phases = phasesCopy;
        }

        public void Initialize()
        {
            _initializedLabel.SetInitialized();

            foreach (IPhase phase in _phases)
            {
                phase.Initialize();
            }
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();

            foreach (IPhase phase in _phases)
            {
                phase.Uninitialize();
            }
        }

        public void Resolve(ResolveContext resolveContext)
        {
            _phaseResolver.Resolve(_phases, resolveContext);
        }
    }
}