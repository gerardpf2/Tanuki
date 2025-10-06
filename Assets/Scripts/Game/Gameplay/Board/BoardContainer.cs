using System.Collections.Generic;
using Game.Common;

namespace Game.Gameplay.Board
{
    public class BoardContainer : IBoardContainer
    {
        private InitializedLabel _initializedLabel;

        public IBoard Board { get; private set; }

        public IEnumerable<PiecePlacement> PiecePlacements { get; private set; }

        public void Initialize(IBoard board, IEnumerable<PiecePlacement> piecePlacements)
        {
            _initializedLabel.SetInitialized();

            Board = board;
            PiecePlacements = piecePlacements;
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();

            Board = null;
            PiecePlacements = null;
        }
    }
}