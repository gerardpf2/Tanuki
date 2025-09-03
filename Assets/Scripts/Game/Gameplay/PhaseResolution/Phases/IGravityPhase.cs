using Game.Gameplay.Board;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public interface IGravityPhase : IPhase
    {
        void Initialize(IBoard board);
    }
}