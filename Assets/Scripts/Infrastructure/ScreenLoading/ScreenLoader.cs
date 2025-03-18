using System;
using Infrastructure.ModelViewViewModel;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.ScreenLoading
{
    public class ScreenLoader : IScreenLoader
    {
        [NotNull] private readonly IScreenDefinitionGetter _screenDefinitionGetter;
        [NotNull] private readonly IScreenPlacementGetter _screenPlacementGetter;

        public ScreenLoader(
            [NotNull] IScreenDefinitionGetter screenDefinitionGetter,
            [NotNull] IScreenPlacementGetter screenPlacementGetter)
        {
            _screenDefinitionGetter = screenDefinitionGetter;
            _screenPlacementGetter = screenPlacementGetter;
        }

        public void Load(string key)
        {
            LoadInstance(key);
        }

        public void Load<T>(string key, T data)
        {
            GameObject instance = LoadInstance(key);

            SetData(instance, data, key);
        }

        [NotNull]
        private GameObject LoadInstance(string key)
        {
            IScreenDefinition screenDefinition = _screenDefinitionGetter.Get(key);

            GameObject instance = Instantiate(screenDefinition);

            if (instance == null)
            {
                throw new InvalidOperationException($"Cannot load screen with Key: {key}");
            }

            return instance;
        }

        private GameObject Instantiate([NotNull] IScreenDefinition screenDefinition)
        {
            GameObject prefab = screenDefinition.Prefab;

            Transform placement = _screenPlacementGetter.Get(screenDefinition.PlacementKey).Transform;

            return Object.Instantiate(prefab, placement);
        }

        private static void SetData<T>([NotNull] GameObject instance, T data, string key)
        {
            if (instance.TryGetComponent(out IDataSettable<T> dataSettable))
            {
                dataSettable.SetData(data);
            }
            else
            {
                throw new InvalidOperationException($"Cannot set data of Type: {typeof(T)} to loaded screen with Key: {key}");
            }
        }
    }
}