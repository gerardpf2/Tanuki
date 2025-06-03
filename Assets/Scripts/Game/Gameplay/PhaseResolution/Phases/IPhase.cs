namespace Game.Gameplay.PhaseResolution.Phases
{
    public interface IPhase
    {
        void Uninitialize();

        void OnBeginIteration();

        ResolveResult Resolve(ResolveContext resolveContext);

        void OnEndIteration();
    }
}