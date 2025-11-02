using UnityEngine;

namespace Infrastructure.Unity.Pooling
{
    public interface IGameObjectPool
    {
        GameObjectPooledInstance Get(GameObject prefab);
    }
}