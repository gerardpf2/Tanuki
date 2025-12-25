using Editor.Game.Gameplay.Editor.UseCases;
using Infrastructure.System.Exceptions;
using UnityEditor;
using UnityEngine;

namespace Editor.Game.Gameplay.Editor
{
    [CreateAssetMenu(fileName = nameof(GameplayEditorLauncher), menuName = "Tanuki/Editor/Game/Gameplay/" + nameof(GameplayEditorLauncher))]
    public class GameplayEditorLauncher : ScriptableObject
    {
        [MenuItem("Window/Tanuki/Editor/Game/Gameplay/" + nameof(GameplayEditorLauncher))]
        public static void LaunchFromMenu()
        {
            GameplayEditorLauncher[] gameplayEditorLaunchers = Resources.FindObjectsOfTypeAll<GameplayEditorLauncher>();

            InvalidOperationException.ThrowIfNull(gameplayEditorLaunchers);

            switch (gameplayEditorLaunchers.Length)
            {
                case <= 0:
                {
                    InvalidOperationException.Throw($"No {nameof(GameplayEditorLauncher)} found");

                    return;
                }
                case > 1:
                {
                    InvalidOperationException.Throw($"Multiple {nameof(GameplayEditorLauncher)} found");

                    return;
                }
                default:
                {
                    GameplayEditorLauncher gameplayEditorLauncher = gameplayEditorLaunchers[0];

                    InvalidOperationException.ThrowIfNull(gameplayEditorLauncher);

                    gameplayEditorLauncher.Launch();

                    break;
                }
            }
        }

        [ContextMenu(nameof(Launch))]
        public void Launch()
        {
            // TODO

            new LoadGameplayEditorUseCase().Resolve();
        }
    }
}