using System;
using System.Collections.Generic;
using Game.Gameplay.PhaseResolution.Phases;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;

namespace Game.Gameplay.PhaseResolution
{
    public class PhaseResolver : IPhaseResolver
    {
        [NotNull, ItemNotNull] private readonly IReadOnlyList<IPhase> _phases;

        public event Action OnEndIteration;

        public PhaseResolver([NotNull, ItemNotNull] params IPhase[] phases)
        {
            ArgumentNullException.ThrowIfNull(phases);

            List<IPhase> phasesCopy = new();

            foreach (IPhase phase in phases)
            {
                ArgumentNullException.ThrowIfNull(phase);

                phasesCopy.Add(phase);
            }

            _phases = phasesCopy;
        }

        public void Initialize()
        {
            Uninitialize();

            foreach (IPhase phase in _phases)
            {
                phase.Initialize();
            }
        }

        public void Uninitialize()
        {
            foreach (IPhase phase in _phases)
            {
                phase.Uninitialize();
            }
        }

        public void Resolve(ResolveContext resolveContext)
        {
            NotifyBeginIteration();

            int index = 0;

            while (index < _phases.Count)
            {
                IPhase phase = _phases[index];

                ResolvePhase(phase, resolveContext, ref index);
            }

            NotifyEndIteration();
        }

        private static void ResolvePhase([NotNull] IPhase phase, ResolveContext resolveContext, ref int index)
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

        private void NotifyBeginIteration()
        {
            foreach (IPhase phase in _phases)
            {
                phase.OnBeginIteration();
            }
        }

        private void NotifyEndIteration()
        {
            foreach (IPhase phase in _phases)
            {
                phase.OnEndIteration();
            }

            OnEndIteration?.Invoke();
        }
    }
}