using Infrastructure.System.Exceptions;
using UnityEditor;

namespace Editor.Game.Gameplay.Editor.UseCases
{
    public class ShowNewGameplayPopupUseCase : IShowNewGameplayPopupUseCase
    {
        public void Resolve()
        {
            NewGameplayWindow newGameplayWindow = EditorWindow.GetWindow<NewGameplayWindow>();

            InvalidOperationException.ThrowIfNull(newGameplayWindow);

            newGameplayWindow.Initialize();
            newGameplayWindow.ShowModalUtility();
        }
    }
}