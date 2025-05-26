using System.Collections.Generic;
using Game.Gameplay.Board;

namespace Game.Gameplay.PhaseResolution
{
    public interface IPhaseResolver
    {
        void Initialize(IBoard board, IEnumerable<PiecePlacement> piecePlacements);

        void Uninitialize();

        void Resolve(ResolveContext resolveContext);
    }
}