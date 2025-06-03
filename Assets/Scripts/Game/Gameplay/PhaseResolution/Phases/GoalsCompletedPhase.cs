namespace Game.Gameplay.PhaseResolution.Phases
{
    public class GoalsCompletedPhase : Phase, IGoalsCompletedPhase
    {
        public GoalsCompletedPhase() : base(1, -1) { }

        public void Initialize()
        {
            // TODO: Remove if not needed

            Uninitialize();
        }

        public override void Uninitialize()
        {
            // TODO: Remove if not needed

            base.Uninitialize();
        }

        protected override bool ResolveImpl(ResolveContext _)
        {
            // TODO

            return false;
        }
    }
}