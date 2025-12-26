using System;

namespace Editor.Game.Gameplay.Editor.UseCases
{
    public interface IShowNewGameplayPopupUseCase
    {
        void Resolve(Action<int> onAccept);
    }
}