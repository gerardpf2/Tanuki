using System;

namespace Game.Gameplay.View.EventResolution.EventResolvers.Actions
{
    public interface IAction
    {
        void Resolve(Action onComplete);
    }
}