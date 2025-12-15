using Game.Gameplay.View.UseCases;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.REMOVE
{
    public class Load : MonoBehaviour
    {
        private ILoadGameplayUseCase _loadGameplayUseCase;

        private void Start()
        {
            InjectResolver.Resolve(this);
        }

        public void Inject([NotNull] ILoadGameplayUseCase loadGameplayUseCase)
        {
            ArgumentNullException.ThrowIfNull(loadGameplayUseCase);

            _loadGameplayUseCase = loadGameplayUseCase;
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
            InvalidOperationException.ThrowIfNull(_loadGameplayUseCase);

            _loadGameplayUseCase.Resolve("Test");
        }
    }
}