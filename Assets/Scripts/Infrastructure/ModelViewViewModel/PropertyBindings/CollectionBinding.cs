using System.Collections.Generic;
using System.Linq;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.ModelViewViewModel.PropertyBindings
{
    public abstract class CollectionBinding<T> : PropertyBinding<IEnumerable<T>>
    {
        [SerializeField] private GameObject _prefab;

        [NotNull] private readonly IDictionary<T, GameObject> _instances = new Dictionary<T, GameObject>();

        public override void Set([ItemNotNull] IEnumerable<T> value)
        {
            value ??= Enumerable.Empty<T>();

            ICollection<T> currentData = new HashSet<T>();

            foreach (T data in value)
            {
                ArgumentNullException.ThrowIfNull(data);

                currentData.Add(data);
            }

            RemoveObsoleteItems(currentData);
            AddItems(currentData);
        }

        private void RemoveObsoleteItems([NotNull] ICollection<T> currentData)
        {
            ArgumentNullException.ThrowIfNull(currentData);

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
            ArgumentNullException.ThrowIfNull(currentData);

            foreach (T data in currentData)
            {
                ArgumentNullException.ThrowIfNull(data);

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
            InvalidOperationException.ThrowIfNull(_prefab);

            GameObject instance = Instantiate(_prefab, transform);

            InvalidOperationException.ThrowIfNull(instance);

            return instance;
        }

        private static void SetData([NotNull] GameObject instance, T data)
        {
            ArgumentNullException.ThrowIfNull(instance);

            IDataSettable<T> dataSettable = instance.GetComponent<IDataSettable<T>>();

            InvalidOperationException.ThrowIfNull(dataSettable);

            dataSettable.SetData(data);
        }
    }
}