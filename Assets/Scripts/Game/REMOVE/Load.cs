using Game.Gameplay.View.UseCases;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

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
            if (Input.GetKeyDown(KeyCode.L))
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