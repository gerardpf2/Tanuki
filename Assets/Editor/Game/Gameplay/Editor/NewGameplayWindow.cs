using System;
using UnityEditor;
using UnityEngine;

namespace Editor.Game.Gameplay.Editor
{
    public class NewGameplayWindow : EditorWindow
    {
        private const int MinColumns = 4;
        private const int MaxColumns = 9;

        private int _columns = MinColumns;
        private Action<int> _onAccept;

        public void Initialize(Action<int> onAccept)
        {
            const string text = nameof(NewGameplayWindow);
            const float width = 300.0f;
            const float height = 50.0f;

            float x = 0.5f * Screen.width;
            float y = 0.5f * Screen.height;

            titleContent = new GUIContent(text);
            position = new Rect(x, y, width, height);

            _onAccept = onAccept;
        }

        private void OnGUI()
        {
            DrawColumnsSlider();
            DrawAcceptButton();
        }

        private void DrawColumnsSlider()
        {
            const string textFormat = "Columns ({0},{1})";

            string text = string.Format(textFormat, MinColumns, MaxColumns);

            _columns = EditorGUILayout.IntSlider(new GUIContent(text), _columns, MinColumns, MaxColumns);
        }

        private void DrawAcceptButton()
        {
            const string text = "Accept";

            if (!GUILayout.Button(text))
            {
                return;
            }

            Close();

            _onAccept?.Invoke(_columns);
        }
    }
}