using Game.Gameplay.View.UseCases;
using Game.GameplayEditor.View.UseCases;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.REMOVE
{
    public class Load : MonoBehaviour
    {
        private ILoadGameplayUseCase _loadGameplayUseCase;
        private ILoadGameplayEditorUseCase _loadGameplayEditorUseCase;

        private void Start()
        {
            InjectResolver.Resolve(this);
        }

        public void Inject(
            [NotNull] ILoadGameplayUseCase loadGameplayUseCase,
            [NotNull] ILoadGameplayEditorUseCase loadGameplayEditorUseCase)
        {
            ArgumentNullException.ThrowIfNull(loadGameplayUseCase);
            ArgumentNullException.ThrowIfNull(loadGameplayEditorUseCase);

            _loadGameplayUseCase = loadGameplayUseCase;
            _loadGameplayEditorUseCase = loadGameplayEditorUseCase;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                LoadGameplay();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                LoadGameplayEditor();
            }
        }

        private void LoadGameplay()
        {
            InvalidOperationException.ThrowIfNull(_loadGameplayUseCase);

            _loadGameplayUseCase.Resolve("Test");
        }

        private void LoadGameplayEditor()
        {
            InvalidOperationException.ThrowIfNull(_loadGameplayEditorUseCase);

            _loadGameplayEditorUseCase.Resolve();
        }
    }
}