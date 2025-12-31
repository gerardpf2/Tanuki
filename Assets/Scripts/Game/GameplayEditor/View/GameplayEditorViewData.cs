using System;

namespace Game.GameplayEditor.View
{
    public class GameplayEditorViewData
    {
        public readonly Action OnReady;

        public GameplayEditorViewData(Action onReady)
        {
            OnReady = onReady;
        }
    }
}