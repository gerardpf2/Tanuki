using Editor.Game.Gameplay.Editor.TopMenu;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEditor;

namespace Editor.Game.Gameplay.Editor.UseCases
{
    public class ShowGameplayEditorUseCase : IShowGameplayEditorUseCase
    {
        [NotNull] private readonly IGameplayEditorTopMenu _gameplayEditorTopMenu;

        public ShowGameplayEditorUseCase([NotNull] IGameplayEditorTopMenu gameplayEditorTopMenu)
        {
            ArgumentNullException.ThrowIfNull(gameplayEditorTopMenu);

            _gameplayEditorTopMenu = gameplayEditorTopMenu;
        }

        public void Resolve()
        {
            GameplayEditorWindow gameplayEditorWindow = EditorWindow.GetWindow<GameplayEditorWindow>();

            InvalidOperationException.ThrowIfNull(gameplayEditorWindow);

            gameplayEditorWindow.Initialize(_gameplayEditorTopMenu);
        }
    }
}