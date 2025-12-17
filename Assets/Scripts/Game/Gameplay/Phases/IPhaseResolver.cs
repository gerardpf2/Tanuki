using System;
using System.Collections.Generic;
using Game.Gameplay.Phases.Phases;

namespace Game.Gameplay.Phases
{
    public interface IPhaseResolver
    {
        event Action<ResolveContext> OnBeginIteration;
        event Action OnEndIteration;

        void Resolve(IReadOnlyList<IPhase> phases, ResolveContext resolveContext);
    }
}