using Infrastructure.DependencyInjection;
using UnityEngine;

namespace Game
{
    public abstract class GameScopeComposerBuilder : ScriptableObject, IGameScopeComposerBuilder
    {
        public abstract IScopeComposer Build();
    }
}