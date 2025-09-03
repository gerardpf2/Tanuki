using Game.Gameplay.Board;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public interface ILineClearPhase : IPhase
    {
        void Initialize(IReadonlyBoard board);
    }
}