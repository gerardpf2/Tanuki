using System;
using Game.Gameplay.Goals;

namespace Game.Gameplay.View.Header.Goals
{
    public interface IGoalsViewContainer : IGoalsContainer
    {
        event Action OnUpdated;
    }
}