using Game.Gameplay.UseCases;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.REMOVE
{
    public class LoadUnloadGameplay : MonoBehaviour
    {
        private ILoadGameplay _loadGameplay;

        private void Start()
        {
            InjectResolver.Resolve(this);
        }

        public void Inject([NotNull] ILoadGameplay loadGameplay)
        {
            ArgumentNullException.ThrowIfNull(loadGameplay);

            _loadGameplay = loadGameplay;
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
            InvalidOperationException.ThrowIfNull(_loadGameplay);

            _loadGameplay.Resolve("Test");
        }

        private void UnloadGameplay()
        {
            // TODO
        }
    }
}