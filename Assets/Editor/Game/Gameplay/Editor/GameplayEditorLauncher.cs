using Editor.Game.Gameplay.Editor.UseCases;
using Game.Common.View.Pieces;
using Infrastructure.System.Exceptions;
using UnityEditor;
using UnityEngine;

namespace Editor.Game.Gameplay.Editor
{
    [CreateAssetMenu(fileName = nameof(GameplayEditorLauncher), menuName = "Tanuki/Editor/Game/Gameplay/" + nameof(GameplayEditorLauncher))]
    public class GameplayEditorLauncher : ScriptableObject
    {
        [SerializeField] private PieceSpriteContainer _pieceSpriteContainer;

        [ContextMenu(nameof(Launch))]
        public void Launch()
        {
            InvalidOperationException.ThrowIfNull(_pieceSpriteContainer);

            ILoadGameplayEditorUseCase loadGameplayEditorUseCase = new LoadGameplayEditorUseCase(_pieceSpriteContainer);

            loadGameplayEditorUseCase.Resolve();
        }

        [MenuItem("Window/Tanuki/Editor/Game/Gameplay/" + nameof(GameplayEditorLauncher))]
        private static void LaunchFromMenu()
        {
            GameplayEditorLauncher[] gameplayEditorLaunchers = Resources.FindObjectsOfTypeAll<GameplayEditorLauncher>();

            InvalidOperationException.ThrowIfNull(gameplayEditorLaunchers);

            switch (gameplayEditorLaunchers.Length)
            {
                case <= 0:
                {
                    InvalidOperationException.Throw($"Cannot find {nameof(GameplayEditorLauncher)}");

                    return;
                }
                case > 1:
                {
                    InvalidOperationException.Throw($"Found multiple {nameof(GameplayEditorLauncher)}");

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
    }
}