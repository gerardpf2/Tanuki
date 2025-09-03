using System;
using Game.Gameplay.Goals;

namespace Game.Gameplay.View.Header.Goals
{
    public class GoalsViewContainer : GoalsContainer, IGoalsViewContainer
    {
        public event Action OnUpdated;

        protected override void HandleCurrentUpdated()
        {
            base.HandleCurrentUpdated();

            OnUpdated?.Invoke();
        }
    }
}