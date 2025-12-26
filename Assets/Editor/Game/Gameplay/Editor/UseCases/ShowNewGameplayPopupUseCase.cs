using System;
using UnityEditor;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Editor.Game.Gameplay.Editor.UseCases
{
    public class ShowNewGameplayPopupUseCase : IShowNewGameplayPopupUseCase
    {
        public void Resolve(Action<int> onAccept)
        {
            NewGameplayWindow newGameplayWindow = EditorWindow.GetWindow<NewGameplayWindow>();

            InvalidOperationException.ThrowIfNull(newGameplayWindow);

            newGameplayWindow.Initialize(onAccept);
            newGameplayWindow.ShowModalUtility();
        }
    }
}