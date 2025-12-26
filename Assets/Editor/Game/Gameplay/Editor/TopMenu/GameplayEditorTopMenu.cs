using UnityEditor;
using UnityEngine;

namespace Editor.Game.Gameplay.Editor.TopMenu
{
    public class GameplayEditorTopMenu : IGameplayEditorTopMenu
    {
        public void Draw()
        {
            GUILayout.BeginHorizontal(EditorStyles.toolbar);

            DrawNewButton();

            GUILayout.FlexibleSpace();

            GUILayout.EndHorizontal();
        }

        private static void DrawNewButton()
        {
            const string text = "New";

            if (GUILayout.Button(text, EditorStyles.toolbarButton))
            {
                // TODO
            }
        }
    }
}