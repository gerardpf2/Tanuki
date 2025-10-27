using Game.Gameplay.UseCases;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.REMOVE
{
    public class LoadUnloadGameplay : MonoBehaviour
    {
        private ILoadGameplayUseCase _loadGameplayUseCase;
        private IUnloadGameplayUseCase _unloadGameplayUseCase;

        private void Start()
        {
            InjectResolver.Resolve(this);
        }

        public void Inject(
            [NotNull] ILoadGameplayUseCase loadGameplayUseCase,
            [NotNull] IUnloadGameplayUseCase unloadGameplayUseCase)
        {
            ArgumentNullException.ThrowIfNull(loadGameplayUseCase);
            ArgumentNullException.ThrowIfNull(unloadGameplayUseCase);

            _loadGameplayUseCase = loadGameplayUseCase;
            _unloadGameplayUseCase = unloadGameplayUseCase;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                LoadGameplay();
            }
            else if (Input.GetKeyDown(KeyCode.U))
            {
                UnloadGameplay();
            }
        }

        private void LoadGameplay()
        {
            InvalidOperationException.ThrowIfNull(_loadGameplayUseCase);

            _loadGameplayUseCase.Resolve("Test");
        }

        private void UnloadGameplay()
        {
            InvalidOperationException.ThrowIfNull(_unloadGameplayUseCase);

            _unloadGameplayUseCase.Resolve();
        }
    }
}