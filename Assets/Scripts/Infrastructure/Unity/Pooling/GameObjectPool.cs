using System.Collections.Generic;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Unity.Pooling
{
    // TODO: Test
    public class GameObjectPool : IGameObjectPool
    {
        [NotNull] private readonly IGameObjectInstantiator _gameObjectInstantiator;

        [NotNull] private readonly IDictionary<GameObject, Queue<GameObject>> _pooledInstancesByPrefab = new Dictionary<GameObject, Queue<GameObject>>();

        public GameObjectPool([NotNull] IGameObjectInstantiator gameObjectInstantiator)
        {
            ArgumentNullException.ThrowIfNull(gameObjectInstantiator);

            _gameObjectInstantiator = gameObjectInstantiator;
        }

        public GameObjectPooledInstance Get([NotNull] GameObject prefab, Transform parent)
        {
            ArgumentNullException.ThrowIfNull(prefab);

            return new GameObjectPooledInstance(prefab, GetInstance(prefab, parent), EnqueueInstance);
        }

        [NotNull]
        private GameObject GetInstance([NotNull] GameObject prefab, Transform parent)
        {
            ArgumentNullException.ThrowIfNull(prefab);

            if (!TryDequeueInstance(prefab, out GameObject instance))
            {
                instance = _gameObjectInstantiator.Instantiate(prefab, parent);
            }

            return instance;
        }

        [ContractAnnotation("=> true, instance:notnull; => false, instance:null")]
        private bool TryDequeueInstance([NotNull] GameObject prefab, out GameObject instance)
        {
            ArgumentNullException.ThrowIfNull(prefab);

            if (_pooledInstancesByPrefab.TryGetValue(prefab, out Queue<GameObject> pooledInstances))
            {
                InvalidOperationException.ThrowIfNull(pooledInstances);

                if (pooledInstances.TryDequeue(out instance))
                {
                    InvalidOperationException.ThrowIfNull(instance);

                    return true;
                }
            }

            instance = null;

            return false;
        }

        private void EnqueueInstance([NotNull] GameObject prefab, [NotNull] GameObject instance)
        {
            ArgumentNullException.ThrowIfNull(prefab);
            ArgumentNullException.ThrowIfNull(instance);

            if (_pooledInstancesByPrefab.TryGetValue(prefab, out Queue<GameObject> pooledInstances))
            {
                InvalidOperationException.ThrowIfNull(pooledInstances);
            }
            else
            {
                pooledInstances = new Queue<GameObject>();

                _pooledInstancesByPrefab.Add(prefab, pooledInstances);
            }

            pooledInstances.Enqueue(instance);
        }
    }
}