using System.Collections.Generic;
using Game.Gameplay.Board;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public interface IInstantiateInitial
    {
        void Resolve(IBoard board, IEnumerable<IPiecePlacement> piecePlacements);
    }
}