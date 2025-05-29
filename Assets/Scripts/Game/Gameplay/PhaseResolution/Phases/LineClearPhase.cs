namespace Game.Gameplay.PhaseResolution.Phases
{
    public class LineClearPhase : Phase, ILineClearPhase
    {
        public LineClearPhase() : base(-1, -1) { }

        public void Initialize() { }

        protected override bool ResolveImpl(ResolveContext resolveContext)
        {
            return false;
        }
    }
}