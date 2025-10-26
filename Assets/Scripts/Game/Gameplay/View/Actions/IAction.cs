using System;

namespace Game.Gameplay.View.Actions
{
    public interface IAction
    {
        void Resolve(Action onComplete);
    }
}