using Editor.Game.Gameplay.Editor.TopMenu;
using Game.Common.View.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEditor;

namespace Editor.Game.Gameplay.Editor.UseCases
{
    public class LoadGameplayEditorUseCase : ILoadGameplayEditorUseCase
    {
        [NotNull] private readonly IGameplayEditorTopMenu _gameplayEditorTopMenu;
        [NotNull] private readonly PieceSpriteContainer _pieceSpriteContainer;

        public LoadGameplayEditorUseCase(
            [NotNull] IGameplayEditorTopMenu gameplayEditorTopMenu,
            [NotNull] PieceSpriteContainer pieceSpriteContainer)
        {
            ArgumentNullException.ThrowIfNull(gameplayEditorTopMenu);
            ArgumentNullException.ThrowIfNull(pieceSpriteContainer);

            _gameplayEditorTopMenu = gameplayEditorTopMenu;
            _pieceSpriteContainer = pieceSpriteContainer;
        }

        public void Resolve()
        {
            if (EditorWindow.HasOpenInstances<GameplayEditorWindow>())
            {
                return;
            }

            GameplayEditorWindow gameplayEditorWindow = EditorWindow.GetWindow<GameplayEditorWindow>();

            InvalidOperationException.ThrowIfNull(gameplayEditorWindow);

            gameplayEditorWindow.Initialize(_gameplayEditorTopMenu, _pieceSpriteContainer);
        }
    }
}