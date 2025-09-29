namespace Game.Gameplay.Goals
{
    public class GoalsContainer : IGoalsContainer
    {
        public IGoals Goals { get; private set; }

        public void Initialize(IGoals goals)
        {
            Uninitialize();

            Goals = goals;
        }

        public void Uninitialize()
        {
            Goals = null;
        }
    }
}