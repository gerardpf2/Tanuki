using System.Collections.Generic;
using Game.Gameplay.Board;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public interface IInitializePhase
    {
        void Resolve(IBoard board, IEnumerable<IPiecePlacement> piecePlacements);
    }
}