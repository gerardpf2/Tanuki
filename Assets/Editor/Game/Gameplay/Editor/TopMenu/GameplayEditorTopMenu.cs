using Editor.Game.Gameplay.Editor.UseCases;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Editor.Game.Gameplay.Editor.TopMenu
{
    public class GameplayEditorTopMenu : IGameplayEditorTopMenu
    {
        [NotNull] private readonly IClearGameplayUseCase _clearGameplayUseCase;

        public GameplayEditorTopMenu([NotNull] IClearGameplayUseCase clearGameplayUseCase)
        {
            ArgumentNullException.ThrowIfNull(clearGameplayUseCase);

            _clearGameplayUseCase = clearGameplayUseCase;
        }

        public void Draw()
        {
            GUILayout.BeginHorizontal(EditorStyles.toolbar);

            DrawClearButton();

            GUILayout.FlexibleSpace();

            GUILayout.EndHorizontal();
        }

        private void DrawClearButton()
        {
            const string text = "Clear";

            if (GUILayout.Button(text, EditorStyles.toolbarButton))
            {
                _clearGameplayUseCase.Resolve();
            }
        }
    }
}