using Game.Common.View.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEditor;

namespace Editor.Game.Gameplay.Editor.UseCases
{
    public class LoadGameplayEditorUseCase : ILoadGameplayEditorUseCase
    {
        [NotNull] private readonly PieceSpriteContainer _pieceSpriteContainer;

        public LoadGameplayEditorUseCase([NotNull] PieceSpriteContainer pieceSpriteContainer)
        {
            ArgumentNullException.ThrowIfNull(pieceSpriteContainer);

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

            gameplayEditorWindow.Initialize(_pieceSpriteContainer);
        }
    }
}