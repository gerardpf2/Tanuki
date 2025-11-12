using System.Collections.Generic;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Unity.Pooling
{
    // TODO: Test
    public class GameObjectPool : IGameObjectPool
    {
        [NotNull] private readonly IDictionary<GameObject, Queue<GameObject>> _pooledInstancesByPrefab = new Dictionary<GameObject, Queue<GameObject>>();
        [NotNull] private readonly Transform _pooledInstancesParent = new GameObject("PooledInstancesParent").transform;

        public GameObjectPooledInstance Get([NotNull] GameObject prefab, Transform parent)
        {
            ArgumentNullException.ThrowIfNull(prefab);

            return new GameObjectPooledInstance(prefab, GetInstance(prefab, parent), EnqueueInstance);
        }

        public void Preload([NotNull] GameObject prefab, int amount, bool onlyIfNeeded)
        {
            ArgumentNullException.ThrowIfNull(prefab);

            if (onlyIfNeeded)
            {
                amount -= GetPooledInstancesAmount(prefab);
            }

            for (int i = 0; i < amount; ++i)
            {
                GameObject instance = Object.Instantiate(prefab);

                InvalidOperationException.ThrowIfNull(instance);

                EnqueueInstance(prefab, instance);
            }
        }

        [NotNull]
        private GameObject GetInstance([NotNull] GameObject prefab, Transform parent)
        {
            ArgumentNullException.ThrowIfNull(prefab);

            if (!TryDequeueInstance(prefab, parent, out GameObject instance))
            {
                instance = Object.Instantiate(prefab, parent);

                InvalidOperationException.ThrowIfNull(instance);
            }

            return instance;
        }

        [ContractAnnotation("=> true, instance:notnull; => false, instance:null")]
        private bool TryDequeueInstance([NotNull] GameObject prefab, Transform parent, out GameObject instance)
        {
            ArgumentNullException.ThrowIfNull(prefab);

            if (_pooledInstancesByPrefab.TryGetValue(prefab, out Queue<GameObject> pooledInstances))
            {
                InvalidOperationException.ThrowIfNull(pooledInstances);

                if (pooledInstances.TryDequeue(out instance))
                {
                    InvalidOperationException.ThrowIfNull(instance);

                    HandleInstanceDequeued(instance, parent);

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

            HandleInstanceEnqueued(instance);
        }

        private static void HandleInstanceDequeued([NotNull] GameObject instance, Transform parent)
        {
            ArgumentNullException.ThrowIfNull(instance);

            instance.transform.SetParent(parent);
            instance.SetActive(true);
        }

        private void HandleInstanceEnqueued([NotNull] GameObject instance)
        {
            ArgumentNullException.ThrowIfNull(instance);

            instance.SetActive(false);
            instance.transform.SetParent(_pooledInstancesParent, false);
        }

        private int GetPooledInstancesAmount([NotNull] GameObject prefab)
        {
            ArgumentNullException.ThrowIfNull(prefab);

            if (!_pooledInstancesByPrefab.TryGetValue(prefab, out Queue<GameObject> pooledInstances))
            {
                return 0;
            }

            InvalidOperationException.ThrowIfNull(pooledInstances);

            return pooledInstances.Count;
        }
    }
}