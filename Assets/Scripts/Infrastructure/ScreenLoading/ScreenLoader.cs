using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
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
            ArgumentNullException.ThrowIfNull(screenDefinitionGetter);
            ArgumentNullException.ThrowIfNull(screenPlacementGetter);

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

            return Instantiate(screenDefinition);;
        }

        [NotNull]
        private GameObject Instantiate([NotNull] IScreenDefinition screenDefinition)
        {
            ArgumentNullException.ThrowIfNull(screenDefinition);

            GameObject prefab = screenDefinition.Prefab;
            Transform placement = _screenPlacementGetter.Get(screenDefinition.PlacementKey).Transform;
            GameObject instance = Object.Instantiate(prefab, placement);

            InvalidOperationException.ThrowIfNullWithMessage(
                instance,
                $"Cannot instantiate screen with Key: {screenDefinition.Key}"
            );

            return instance;
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