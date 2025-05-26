using System.Collections.Generic;
using Game.Gameplay.Board;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public interface IInstantiateInitialPiecesPhase : IPhase
    {
        void Initialize(IBoard board, IEnumerable<PiecePlacement> piecePlacements);

        void Uninitialize();
    }
}