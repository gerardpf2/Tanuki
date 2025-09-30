using System;

namespace Game.Gameplay.PhaseResolution
{
    public interface IPhaseResolver
    {
        event Action OnEndIteration;

        void Initialize();

        void Uninitialize();

        void Resolve(ResolveContext resolveContext);
    }
}