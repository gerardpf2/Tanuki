using System.Collections.Generic;
using Game.Gameplay.Model.Board;

namespace Game.Gameplay.Model.PhaseResolution.Phases
{
    public interface IInitializePhase
    {
        void Resolve(IBoard board, IEnumerable<IPiecePlacement> piecePlacements);
    }
}