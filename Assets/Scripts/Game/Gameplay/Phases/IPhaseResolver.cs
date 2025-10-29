using System;

namespace Game.Gameplay.Phases
{
    public interface IPhaseResolver
    {
        event Action OnEndIteration;

        void Initialize();

        void Uninitialize();

        void Resolve(ResolveContext resolveContext);
    }
}