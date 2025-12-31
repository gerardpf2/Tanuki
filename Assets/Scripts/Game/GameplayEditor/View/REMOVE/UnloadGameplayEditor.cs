using Game.GameplayEditor.View.UseCases;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.GameplayEditor.View.REMOVE
{
    public class UnloadGameplayEditor : MonoBehaviour
    {
        private IUnloadGameplayEditorUseCase _unloadGameplayEditorUseCase;

        private void Awake()
        {
            InjectResolver.Resolve(this);
        }

        public void Inject([NotNull] IUnloadGameplayEditorUseCase unloadGameplayEditorUseCase)
        {
            ArgumentNullException.ThrowIfNull(unloadGameplayEditorUseCase);

            _unloadGameplayEditorUseCase = unloadGameplayEditorUseCase;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                Unload();
            }
        }

        private void Unload()
        {
            InvalidOperationException.ThrowIfNull(_unloadGameplayEditorUseCase);

            _unloadGameplayEditorUseCase.Resolve();
        }
    }
}