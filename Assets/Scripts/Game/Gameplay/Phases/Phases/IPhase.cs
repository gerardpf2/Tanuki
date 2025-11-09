namespace Game.Gameplay.Phases.Phases
{
    public interface IPhase
    {
        void OnBeginIteration();

        ResolveResult Resolve(ResolveContext resolveContext);

        void OnEndIteration();
    }
}