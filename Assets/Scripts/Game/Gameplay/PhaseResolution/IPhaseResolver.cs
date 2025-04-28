using System.Collections.Generic;
using Game.Gameplay.Board;

namespace Game.Gameplay.PhaseResolution
{
    public interface IPhaseResolver
    {
        void ResolveInstantiateInitial(IBoard board, IEnumerable<IPiecePlacement> piecePlacements);

        void Resolve(IBoard board);
    }
}