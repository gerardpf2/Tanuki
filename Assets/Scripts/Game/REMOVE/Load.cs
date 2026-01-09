using Game.Gameplay.View.UseCases;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.REMOVE
{
    public class Load : MonoBehaviour
    {
        private IInitializeAndLoadAndRunGameplayUseCase _initializeAndLoadAndRunGameplayUseCase;

        private void Start()
        {
            InjectResolver.Resolve(this);
        }

        public void Inject([NotNull] IInitializeAndLoadAndRunGameplayUseCase initializeAndLoadAndRunGameplayUseCase)
        {
            ArgumentNullException.ThrowIfNull(initializeAndLoadAndRunGameplayUseCase);

            _initializeAndLoadAndRunGameplayUseCase = initializeAndLoadAndRunGameplayUseCase;
        }

        private void Update()
        {
            if (Keyboard.current?.lKey.wasPressedThisFrame ?? false)
            {
                LoadGameplay();
            }
        }

        private void LoadGameplay()
        {
            InvalidOperationException.ThrowIfNull(_initializeAndLoadAndRunGameplayUseCase);

            _initializeAndLoadAndRunGameplayUseCase.Resolve("Test");
        }
    }
}