using Editor.Game.Gameplay.Editor.TopMenu;
using Game.Common.View.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEditor;

namespace Editor.Game.Gameplay.Editor
{
    public class GameplayEditorWindow : EditorWindow
    {
        private IGameplayEditorTopMenu _gameplayEditorTopMenu;
        private PieceSpriteContainer _pieceSpriteContainer; // TODO

        public void Initialize(
            [NotNull] IGameplayEditorTopMenu gameplayEditorTopMenu,
            [NotNull] PieceSpriteContainer pieceSpriteContainer)
        {
            ArgumentNullException.ThrowIfNull(gameplayEditorTopMenu);
            ArgumentNullException.ThrowIfNull(pieceSpriteContainer);

            Uninitialize();

            _gameplayEditorTopMenu = gameplayEditorTopMenu;
            _pieceSpriteContainer = pieceSpriteContainer;
        }

        private void Uninitialize()
        {
            _gameplayEditorTopMenu = null;
            _pieceSpriteContainer = null;
        }

        private void OnGUI()
        {
            InvalidOperationException.ThrowIfNull(_gameplayEditorTopMenu);

            _gameplayEditorTopMenu.Draw();
        }
    }
}