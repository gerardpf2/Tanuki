using System;

namespace Game.Gameplay.View.Board
{
    public class BoardViewData
    {
        public readonly Action OnReady;

        public BoardViewData(Action onReady)
        {
            OnReady = onReady;
        }
    }
}