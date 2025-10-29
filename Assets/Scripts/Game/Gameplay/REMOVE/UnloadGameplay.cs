using Game.Gameplay.UseCases;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.REMOVE
{
    public class UnloadGameplay : MonoBehaviour
    {
        private IUnloadGameplayUseCase _unloadGameplayUseCase;

        private void Awake()
        {
            InjectResolver.Resolve(this);
        }

        public void Inject([NotNull] IUnloadGameplayUseCase unloadGameplayUseCase)
        {
            ArgumentNullException.ThrowIfNull(unloadGameplayUseCase);

            _unloadGameplayUseCase = unloadGameplayUseCase;
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
            InvalidOperationException.ThrowIfNull(_unloadGameplayUseCase);

            _unloadGameplayUseCase.Resolve();
        }
    }
}