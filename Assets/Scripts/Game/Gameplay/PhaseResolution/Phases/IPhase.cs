namespace Game.Gameplay.PhaseResolution.Phases
{
    public interface IPhase
    {
        void Uninitialize();

        void OnBeginIteration();

        bool Resolve(ResolveContext resolveContext);

        void OnEndIteration();
    }
}