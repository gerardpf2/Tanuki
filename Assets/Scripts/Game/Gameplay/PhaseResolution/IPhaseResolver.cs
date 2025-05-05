using System.Collections.Generic;
using Game.Gameplay.Board;

namespace Game.Gameplay.PhaseResolution
{
    public interface IPhaseResolver
    {
        void Initialize(IBoard board, IEnumerable<IPiecePlacement> piecePlacements);

        void Resolve();
    }
}