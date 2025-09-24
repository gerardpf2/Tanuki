namespace Game.Gameplay.PhaseResolution
{
    public interface IPhaseResolver
    {
        void Initialize();

        void Uninitialize();

        void Resolve(ResolveContext resolveContext);
    }
}