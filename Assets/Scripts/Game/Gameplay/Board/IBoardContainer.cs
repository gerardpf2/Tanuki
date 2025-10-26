using System.Collections.Generic;
using Game.Gameplay.Pieces;

namespace Game.Gameplay.Board
{
    public interface IBoardContainer
    {
        IBoard Board { get; }

        IEnumerable<PiecePlacement> PiecePlacements { get; }

        void Initialize(IBoard board, IEnumerable<PiecePlacement> piecePlacements);

        void Uninitialize();
    }
}