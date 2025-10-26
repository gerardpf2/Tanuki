using System;

namespace Game.Gameplay.View.Actions.Actions
{
    public interface IAction
    {
        void Resolve(Action onComplete);
    }
}