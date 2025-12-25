using UnityEditor;

namespace Editor.Game.Gameplay.Editor.View.UseCases
{
    public class LoadGameplayEditorUseCase : ILoadGameplayEditorUseCase
    {
        public void Resolve()
        {
            EditorWindow.GetWindow(typeof(GameplayEditor));
        }
    }
}