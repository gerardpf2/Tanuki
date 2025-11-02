using System;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Infrastructure.Unity.Pooling
{
    public struct GameObjectPooledInstance
    {
        [NotNull] private readonly GameObject _prefab;
        [NotNull] public readonly GameObject Instance;
        private readonly Action<GameObject, GameObject> _onReturnToPool;

        private bool _hasBeenReturnedToPool;

        public GameObjectPooledInstance(
            [NotNull] GameObject prefab,
            [NotNull] GameObject instance,
            Action<GameObject, GameObject> onReturnToPool)
        {
            ArgumentNullException.ThrowIfNull(prefab);
            ArgumentNullException.ThrowIfNull(instance);

            _prefab = prefab;
            Instance = instance;
            _onReturnToPool = onReturnToPool;

            _hasBeenReturnedToPool = false;
        }

        public void ReturnToPool()
        {
            if (_hasBeenReturnedToPool)
            {
                InvalidOperationException.Throw($"Instance {Instance.name} has already been returned to pool");
            }

            _hasBeenReturnedToPool = true;

            _onReturnToPool?.Invoke(_prefab, Instance);
        }
    }
}