namespace Game.Gameplay.Goals
{
    public interface IGoalsContainer
    {
        IGoals Goals { get; }

        void Initialize(IGoals goals);

        void Uninitialize();
    }
}