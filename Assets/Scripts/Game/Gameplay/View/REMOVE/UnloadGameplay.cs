using Game.Gameplay.View.UseCases;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

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
            if (Input.GetKeyDown(KeyCode.U))
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