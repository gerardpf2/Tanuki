using Game.Gameplay.View.UseCases;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Gameplay.View.REMOVE
{
    public class UnloadGameplay : MonoBehaviour
    {
        private IUninitializeAndUnloadGameplayUseCase _uninitializeAndUnloadGameplayUseCase;

        private void Awake()
        {
            InjectResolver.Resolve(this);
        }

        public void Inject([NotNull] IUninitializeAndUnloadGameplayUseCase uninitializeAndUnloadGameplayUseCase)
        {
            ArgumentNullException.ThrowIfNull(uninitializeAndUnloadGameplayUseCase);

            _uninitializeAndUnloadGameplayUseCase = uninitializeAndUnloadGameplayUseCase;
        }

        private void Update()
        {
            if (Keyboard.current?.uKey.wasPressedThisFrame ?? false)
            {
                Unload();
            }
        }

        private void Unload()
        {
            InvalidOperationException.ThrowIfNull(_uninitializeAndUnloadGameplayUseCase);

            _uninitializeAndUnloadGameplayUseCase.Resolve();
        }
    }
}