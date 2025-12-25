using Game.Common.View.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEditor;

namespace Editor.Game.Gameplay.Editor
{
    public class GameplayEditorWindow : EditorWindow
    {
        public void Initialize([NotNull] PieceSpriteContainer pieceSpriteContainer)
        {
            ArgumentNullException.ThrowIfNull(pieceSpriteContainer);

            // TODO
        }

        private void OnGUI()
        {
            // TODO
        }
    }
}