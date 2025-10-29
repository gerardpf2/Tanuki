namespace Game.Gameplay.Phases.Phases
{
    public interface IPhase
    {
        void Initialize();

        void Uninitialize();

        void OnBeginIteration();

        ResolveResult Resolve(ResolveContext resolveContext);

        void OnEndIteration();
    }
}