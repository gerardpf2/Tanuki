namespace Game.Gameplay.PhaseResolution.Phases
{
    public abstract class Phase : IPhase
    {
        public virtual void OnBeginIteration() { }

        public abstract bool Resolve();

        public virtual void OnEndIteration() { }
    }
}