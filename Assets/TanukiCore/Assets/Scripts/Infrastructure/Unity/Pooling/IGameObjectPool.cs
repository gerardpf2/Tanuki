using UnityEngine;

namespace Infrastructure.Unity.Pooling
{
    public interface IGameObjectPool
    {
        GameObjectPooledInstance Get(GameObject prefab, Transform parent);

        void Preload(GameObject prefab, int amount, bool onlyIfNeeded);
    }
}