using UnityEditor;

namespace Editor.Game.Gameplay.Editor.UseCases
{
    public class LoadGameplayEditorUseCase : ILoadGameplayEditorUseCase
    {
        public void Resolve()
        {
            EditorWindow.GetWindow(typeof(GameplayEditorWindow));
        }
    }
}