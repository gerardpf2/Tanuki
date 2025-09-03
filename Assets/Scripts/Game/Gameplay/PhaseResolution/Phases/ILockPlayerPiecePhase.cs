using Game.Gameplay.Board;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public interface ILockPlayerPiecePhase : IPhase
    {
        void Initialize(IBoard board);
    }
}