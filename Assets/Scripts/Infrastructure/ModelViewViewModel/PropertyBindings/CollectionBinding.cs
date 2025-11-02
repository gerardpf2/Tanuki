using System.Collections.Generic;
using System.Linq;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity;
using Infrastructure.Unity.Utils;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.ModelViewViewModel.PropertyBindings
{
    /*
     *
     * CollectionBinding is a template class, and inject cannot be resolved as usual
     * CollectionBinding dependencies are injected to CollectionBindingDependenciesContainer instead
     *
     */
    public class CollectionBindingDependenciesContainer
    {
        public IGameObjectInstantiator GameObjectInstantiator { get; private set; }

        public void Inject(IGameObjectInstantiator gameObjectInstantiator)
        {
            GameObjectInstantiator = gameObjectInstantiator;
        }
    }

    public abstract class CollectionBinding<T> : PropertyBinding<IEnumerable<T>>
    {
        [SerializeField] private GameObject _prefab;

        [NotNull] private readonly CollectionBindingDependenciesContainer _collectionBindingDependenciesContainer = new();
        [NotNull] private readonly IDictionary<T, GameObject> _instances = new Dictionary<T, GameObject>();

        private IGameObjectInstantiator GameObjectInstantiator => _collectionBindingDependenciesContainer.GameObjectInstantiator;

        private void Awake()
        {
            InjectResolver.Resolve(_collectionBindingDependenciesContainer);
        }

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
            InvalidOperationException.ThrowIfNull(GameObjectInstantiator);

            foreach (T data in _instances.Keys.ToList())
            {
                if (currentData.Contains(data))
                {
                    continue;
                }

                GameObjectInstantiator.Destroy(_instances[data]);

                _instances.Remove(data);
            }
        }

        private void AddItems([NotNull, ItemNotNull] IEnumerable<T> currentData)
        {
            ArgumentNullException.ThrowIfNull(currentData);
            InvalidOperationException.ThrowIfNull(GameObjectInstantiator);

            foreach (T data in currentData)
            {
                ArgumentNullException.ThrowIfNull(data);

                if (!_instances.TryGetValue(data, out GameObject instance))
                {
                    instance = GameObjectInstantiator.Instantiate(_prefab).WithParent(transform, false);

                    _instances.Add(data, instance);
                }

                SetData(instance, data);

                instance.transform.SetAsLastSibling();
            }
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