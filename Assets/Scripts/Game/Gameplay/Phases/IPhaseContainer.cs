namespace Game.Gameplay.Phases
{
    public interface IPhaseContainer
    {
        void Initialize();

        void Uninitialize();

        void Resolve(ResolveContext resolveContext);
    }
}