using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Game.Gameplay.Editor
{
    public class NewGameplayWindow : EditorWindow
    {
        public void Initialize()
        {
            const string text = nameof(NewGameplayWindow);
            const float width = 200.0f;
            const float height = 200.0f;

            titleContent = new GUIContent(text);
            position = new Rect(0.5f * Screen.width, 0.5f * Screen.height, width, height);
        }

        private void CreateGUI()
        {
            const string acceptButtonText = "Accept";

            Button acceptButton = new() { text = acceptButtonText };

            rootVisualElement.Add(acceptButton);
        }
    }
}