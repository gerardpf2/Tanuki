using System;
using System.Collections.Generic;
using Game.Gameplay.Phases.Phases;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;

namespace Game.Gameplay.Phases
{
    public class PhaseResolver : IPhaseResolver
    {
        public event Action<ResolveContext> OnBeginIteration;
        public event Action OnEndIteration;

        public void Resolve([NotNull, ItemNotNull] IReadOnlyList<IPhase> phases, ResolveContext resolveContext)
        {
            ArgumentNullException.ThrowIfNull(phases);

            foreach (IPhase phase in phases)
            {
                ArgumentNullException.ThrowIfNull(phase);
            }

            NotifyBeginIteration(phases, resolveContext);

            int index = 0;

            while (index < phases.Count)
            {
                IPhase phase = phases[index];

                ResolveSingle(phase, resolveContext, ref index);
            }

            NotifyEndIteration(phases);
        }

        private static void ResolveSingle([NotNull] IPhase phase, ResolveContext resolveContext, ref int index)
        {
            ArgumentNullException.ThrowIfNull(phase);

            ResolveResult resolveResult = phase.Resolve(resolveContext);

            switch (resolveResult)
            {
                case ResolveResult.Updated:
                    index = 0;
                    break;
                case ResolveResult.NotUpdated:
                    ++index;
                    break;
                case ResolveResult.Stop:
                    index = int.MaxValue;
                    break;
                default:
                    ArgumentOutOfRangeException.Throw(resolveResult);
                    return;
            }
        }

        private void NotifyBeginIteration(
            [NotNull, ItemNotNull] IEnumerable<IPhase> phases,
            ResolveContext resolveContext)
        {
            ArgumentNullException.ThrowIfNull(phases);

            foreach (IPhase phase in phases)
            {
                ArgumentNullException.ThrowIfNull(phase);

                phase.OnBeginIteration();
            }

            OnBeginIteration?.Invoke(resolveContext);
        }

        private void NotifyEndIteration([NotNull, ItemNotNull] IEnumerable<IPhase> phases)
        {
            ArgumentNullException.ThrowIfNull(phases);

            foreach (IPhase phase in phases)
            {
                ArgumentNullException.ThrowIfNull(phase);

                phase.OnEndIteration();
            }

            OnEndIteration?.Invoke();
        }
    }
}