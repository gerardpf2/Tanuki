using System.Collections.Generic;
using Game.Gameplay.Board;

namespace Game.Gameplay.PhaseResolution
{
    // TODO: Rename plural
    public interface IPhaseResolver
    {
        void InitializeAndResolve(IBoard board, IEnumerable<IPiecePlacement> piecePlacements);

        void Resolve(IBoard board);
    }
}