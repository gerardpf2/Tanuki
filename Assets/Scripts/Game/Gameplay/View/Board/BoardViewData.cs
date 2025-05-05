using System;
using Game.Gameplay.Board;

namespace Game.Gameplay.View.Board
{
    public class BoardViewData
    {
        public readonly IReadonlyBoard Board;
        public readonly Action OnViewReady;

        public BoardViewData(IReadonlyBoard board, Action onViewReady)
        {
            Board = board;
            OnViewReady = onViewReady;
        }
    }
}