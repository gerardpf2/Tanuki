using Game.Common;

namespace Game.Gameplay.Goals
{
    public class GoalsContainer : IGoalsContainer
    {
        private InitializedLabel _initializedLabel;

        public IGoals Goals { get; private set; }

        public void Initialize(IGoals goals)
        {
            _initializedLabel.SetInitialized();

            Goals = goals;
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();

            Goals = null;
        }
    }
}