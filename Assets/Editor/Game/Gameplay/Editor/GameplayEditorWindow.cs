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

        public void Initialize([NotNull] PieceSpriteContainer pieceSpriteContainer)
        {
            ArgumentNullException.ThrowIfNull(pieceSpriteContainer);

            // TODO

            _gameplayEditorTopMenu = new GameplayEditorTopMenu();
        }

        private void OnGUI()
        {
            InvalidOperationException.ThrowIfNull(_gameplayEditorTopMenu);

            _gameplayEditorTopMenu.Draw();
        }
    }
}