namespace Game.Gameplay.PhaseResolution.Phases
{
    public interface IInstantiatePlayerPiecePhase : IPhase
    {
        void Initialize();

        void Uninitialize();
    }
}