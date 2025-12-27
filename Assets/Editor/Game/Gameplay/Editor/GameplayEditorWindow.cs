using Editor.Game.Gameplay.Editor.TopMenu;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEditor;

namespace Editor.Game.Gameplay.Editor
{
    public class GameplayEditorWindow : EditorWindow
    {
        private IGameplayEditorTopMenu _gameplayEditorTopMenu;

        public void Initialize([NotNull] IGameplayEditorTopMenu gameplayEditorTopMenu)
        {
            ArgumentNullException.ThrowIfNull(gameplayEditorTopMenu);

            Uninitialize();

            _gameplayEditorTopMenu = gameplayEditorTopMenu;
        }

        private void Uninitialize()
        {
            _gameplayEditorTopMenu = null;
        }

        private void OnGUI()
        {
            InvalidOperationException.ThrowIfNull(_gameplayEditorTopMenu);

            _gameplayEditorTopMenu.Draw();
        }
    }
}