using Editor.Game.Gameplay.Editor.UseCases;
using UnityEngine;

namespace Editor.Game.Gameplay.Editor
{
    [CreateAssetMenu(fileName = nameof(GameplayEditorLauncher), menuName = "Tanuki/Editor/Game/Gameplay/" + nameof(GameplayEditorLauncher))]
    public class GameplayEditorLauncher : ScriptableObject
    {
        [ContextMenu(nameof(Launch))]
        private void Launch()
        {
            // TODO

            new LoadGameplayEditorUseCase().Resolve();
        }
    }
}