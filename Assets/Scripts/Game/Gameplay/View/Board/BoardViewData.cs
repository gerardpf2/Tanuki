using System;

namespace Game.Gameplay.View.Board
{
    // TODO: Remove if not needed
    public class BoardViewData
    {
        public readonly Action OnViewReady;

        public BoardViewData(Action onViewReady)
        {
            OnViewReady = onViewReady;
        }
    }
}