using System.Collections.Generic;
using Game.Gameplay.Board;

namespace Game.Gameplay.PhaseResolution
{
    public interface IPhaseResolver
    {
        void ResolveInstantiateInitialAndCascade(IBoard board, IEnumerable<IPiecePlacement> piecePlacements);

        void ResolveCascade(IBoard board);
    }
}