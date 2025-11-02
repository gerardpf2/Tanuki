using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Unity
{
    public interface IGameObjectInstantiator
    {
        [NotNull]
        GameObject Instantiate(GameObject prefab);

        void Destroy(GameObject instance);
    }
}