using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.ModelViewViewModel.PropertyBindings
{
    public abstract class CollectionBinding<T> : PropertyBinding<IEnumerable<T>>
    {
        // TODO: Exceptions

        [SerializeField] private GameObject _prefab;

        [NotNull] private readonly IDictionary<T, GameObject> _instances = new Dictionary<T, GameObject>();

        public override void Set([ItemNotNull] IEnumerable<T> value)
        {
            ICollection<T> currentData = new HashSet<T>(value ?? Enumerable.Empty<T>());

            RemoveObsoleteItems(currentData);
            AddItems(currentData);
        }

        private void RemoveObsoleteItems([NotNull] ICollection<T> currentData)
        {
            foreach (T data in _instances.Keys.ToList())
            {
                if (currentData.Contains(data))
                {
                    continue;
                }

                Destroy(_instances[data]);

                _instances.Remove(data);
            }
        }

        private void AddItems([NotNull] [ItemNotNull] IEnumerable<T> currentData)
        {
            foreach (T data in currentData)
            {
                if (!_instances.TryGetValue(data, out GameObject instance))
                {
                    instance = Instantiate();

                    _instances.Add(data, instance);
                }

                SetData(instance, data);

                instance.transform.SetAsLastSibling();
            }
        }

        [NotNull]
        private GameObject Instantiate()
        {
            GameObject instance = Instantiate(_prefab, transform);

            if (!instance)
            {
                throw new InvalidOperationException("Cannot instantiate item");
            }

            return instance;
        }

        private static void SetData([NotNull] GameObject instance, T data)
        {
            if (instance.TryGetComponent(out IDataSettable<T> dataSettable))
            {
                dataSettable.SetData(data);
            }
            else
            {
                throw new InvalidOperationException($"Cannot set data of Type: {typeof(T)} to item");
            }
        }
    }
}