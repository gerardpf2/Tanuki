using System;

namespace Game.Gameplay.View
{
    public class GameplayViewData
    {
        public readonly Action OnReady;

        public GameplayViewData(Action onReady)
        {
            OnReady = onReady;
        }
    }
}