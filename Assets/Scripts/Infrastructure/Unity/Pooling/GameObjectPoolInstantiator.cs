using System.Collections.Generic;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Unity.Pooling
{
    public class GameObjectPoolInstantiator : IGameObjectInstantiator
    {
        [NotNull] private readonly IGameObjectPool _gameObjectPool;

        [NotNull] private readonly IDictionary<GameObject, GameObjectPooledInstance> _pooledInstances = new Dictionary<GameObject, GameObjectPooledInstance>();

        public GameObjectPoolInstantiator([NotNull] IGameObjectPool gameObjectPool)
        {
            ArgumentNullException.ThrowIfNull(gameObjectPool);

            _gameObjectPool = gameObjectPool;
        }

        public GameObject Instantiate(GameObject prefab)
        {
            GameObjectPooledInstance gameObjectPooledInstance = _gameObjectPool.Get(prefab);
            GameObject instance = gameObjectPooledInstance.Instance;

            if (_pooledInstances.ContainsKey(instance))
            {
                InvalidOperationException.Throw($"Instance {instance.name} has already been added");
            }

            _pooledInstances.Add(instance, gameObjectPooledInstance);

            return instance;
        }

        public void Destroy([NotNull] GameObject instance)
        {
            ArgumentNullException.ThrowIfNull(instance);

            if (!_pooledInstances.TryGetValue(instance, out GameObjectPooledInstance gameObjectPooledInstance))
            {
                InvalidOperationException.Throw($"Instance {instance.name} cannot be found");
            }

            _pooledInstances.Remove(instance);

            gameObjectPooledInstance.ReturnToPool();
        }
    }
}