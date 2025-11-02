using System.Collections.Generic;
using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity;
using Infrastructure.Unity.Utils;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.ScreenLoading
{
    public class ScreenLoader : IScreenLoader
    {
        [NotNull] private readonly IScreenDefinitionGetter _screenDefinitionGetter;
        [NotNull] private readonly IScreenPlacementGetter _screenPlacementGetter;
        [NotNull] private readonly IGameObjectInstantiator _gameObjectInstantiator;

        [NotNull] private readonly IDictionary<string, GameObject> _loaded = new Dictionary<string, GameObject>();

        public ScreenLoader(
            [NotNull] IScreenDefinitionGetter screenDefinitionGetter,
            [NotNull] IScreenPlacementGetter screenPlacementGetter,
            [NotNull] IGameObjectInstantiator gameObjectInstantiator)
        {
            ArgumentNullException.ThrowIfNull(screenDefinitionGetter);
            ArgumentNullException.ThrowIfNull(screenPlacementGetter);
            ArgumentNullException.ThrowIfNull(gameObjectInstantiator);

            _screenDefinitionGetter = screenDefinitionGetter;
            _screenPlacementGetter = screenPlacementGetter;
            _gameObjectInstantiator = gameObjectInstantiator;
        }

        public void Load([NotNull] string key)
        {
            ArgumentNullException.ThrowIfNull(key);

            LoadAndAddRef(key);
        }

        public void Load<T>([NotNull] string key, T data)
        {
            ArgumentNullException.ThrowIfNull(key);

            GameObject instance = LoadAndAddRef(key);

            SetData(instance, data, key);
        }

        public void Unload([NotNull] string key)
        {
            ArgumentNullException.ThrowIfNull(key);

            UnloadAndRemoveRef(key);
        }

        [NotNull]
        private GameObject LoadAndAddRef([NotNull] string key)
        {
            ArgumentNullException.ThrowIfNull(key);

            if (!_loaded.TryGetValue(key, out GameObject instance))
            {
                IScreenDefinition screenDefinition = _screenDefinitionGetter.Get(key);

                instance = Instantiate(screenDefinition);

                _loaded.Add(key, instance);
            }
            else
            {
                InvalidOperationException.ThrowIfNull(instance);
            }

            return instance;
        }

        private void UnloadAndRemoveRef([NotNull] string key)
        {
            ArgumentNullException.ThrowIfNull(key);

            if (!_loaded.TryGetValue(key, out GameObject instance))
            {
                return;
            }

            _gameObjectInstantiator.Destroy(instance);

            _loaded.Remove(key);
        }

        [NotNull]
        private GameObject Instantiate([NotNull] IScreenDefinition screenDefinition)
        {
            ArgumentNullException.ThrowIfNull(screenDefinition);

            GameObject prefab = screenDefinition.Prefab;
            Transform placement = _screenPlacementGetter.Get(screenDefinition.PlacementKey).Transform;

            return _gameObjectInstantiator.Instantiate(prefab).WithParent(placement);
        }

        private static void SetData<T>([NotNull] GameObject instance, T data, string key)
        {
            ArgumentNullException.ThrowIfNull(instance);

            IDataSettable<T> dataSettable = instance.GetComponent<IDataSettable<T>>();

            InvalidOperationException.ThrowIfNullWithMessage(
                dataSettable,
                $"Cannot set data of Type: {typeof(T)} to screen with Key: {key}"
            );

            dataSettable.SetData(data);
        }
    }
}