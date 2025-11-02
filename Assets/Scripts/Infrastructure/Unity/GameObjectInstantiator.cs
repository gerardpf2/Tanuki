using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Unity
{
    // TODO: Test
    public class GameObjectInstantiator : IGameObjectInstantiator
    {
        public GameObject Instantiate([NotNull] GameObject prefab)
        {
            ArgumentNullException.ThrowIfNull(prefab);

            GameObject instance = Object.Instantiate(prefab);

            InvalidOperationException.ThrowIfNull(instance);

            return instance;
        }

        public void Destroy([NotNull] GameObject instance)
        {
            ArgumentNullException.ThrowIfNull(instance);

            Object.Destroy(instance);
        }
    }
}