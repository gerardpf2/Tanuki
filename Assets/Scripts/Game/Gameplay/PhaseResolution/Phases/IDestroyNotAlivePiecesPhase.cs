using Game.Gameplay.Board;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public interface IDestroyNotAlivePiecesPhase : IPhase
    {
        void Initialize(IBoard board);
    }
}