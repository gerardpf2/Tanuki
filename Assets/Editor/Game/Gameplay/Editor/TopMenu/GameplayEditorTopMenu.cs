using Editor.Game.Gameplay.Editor.UseCases;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Editor.Game.Gameplay.Editor.TopMenu
{
    public class GameplayEditorTopMenu : IGameplayEditorTopMenu
    {
        [NotNull] private readonly INewGameplayUseCase _newGameplayUseCase;

        public GameplayEditorTopMenu([NotNull] INewGameplayUseCase newGameplayUseCase)
        {
            ArgumentNullException.ThrowIfNull(newGameplayUseCase);

            _newGameplayUseCase = newGameplayUseCase;
        }

        public void Draw()
        {
            GUILayout.BeginHorizontal(EditorStyles.toolbar);

            DrawNewButton();

            GUILayout.FlexibleSpace();

            GUILayout.EndHorizontal();
        }

        private void DrawNewButton()
        {
            const string text = "New";

            if (GUILayout.Button(text, EditorStyles.toolbarButton))
            {
                _newGameplayUseCase.Resolve();
            }
        }
    }
}