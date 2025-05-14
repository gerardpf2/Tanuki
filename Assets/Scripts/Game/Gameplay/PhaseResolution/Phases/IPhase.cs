namespace Game.Gameplay.PhaseResolution.Phases
{
    public interface IPhase
    {
        void OnBeginIteration();

        bool Resolve();

        void OnEndIteration();
    }
}