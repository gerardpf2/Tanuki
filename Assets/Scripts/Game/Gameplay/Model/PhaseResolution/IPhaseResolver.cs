using System.Collections.Generic;
using Game.Gameplay.Model.Board;

namespace Game.Gameplay.Model.PhaseResolution
{
    public interface IPhaseResolver
    {
        void InitializeAndResolve(IBoard board, IEnumerable<IPiecePlacement> piecePlacements);

        void Resolve(IBoard board);
    }
}