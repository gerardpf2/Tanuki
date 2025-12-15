using System;

namespace Game.Gameplay.View.EventResolvers
{
    public interface IEventsResolver
    {
        event Action OnResolveBegin;
        event Action OnResolveEnd;

        bool Resolving { get; }

        void Initialize();

        void Uninitialize();
    }
}