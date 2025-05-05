using System;

namespace Game.Gameplay.View.Board
{
    public class BoardViewData
    {
        public readonly Action OnViewReady;

        public BoardViewData(Action onViewReady)
        {
            OnViewReady = onViewReady;
        }
    }
}