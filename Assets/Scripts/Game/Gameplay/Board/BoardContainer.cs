using System.Collections.Generic;

namespace Game.Gameplay.Board
{
    public class BoardContainer : IBoardContainer
    {
        public IBoard Board { get; private set; }

        public IEnumerable<PiecePlacement> PiecePlacements { get; private set; }

        public void Initialize(IBoard board, IEnumerable<PiecePlacement> piecePlacements)
        {
            Uninitialize();

            Board = board;
            PiecePlacements = piecePlacements;
        }

        public void Uninitialize()
        {
            Board = null;
            PiecePlacements = null;
        }
    }
}