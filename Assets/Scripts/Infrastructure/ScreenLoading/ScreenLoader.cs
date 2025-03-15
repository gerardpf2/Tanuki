using Infrastructure.ModelViewViewModel;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.ScreenLoading
{
    public class ScreenLoader : IScreenLoader
    {
        private readonly IScreenDefinitionGetter _screenDefinitionGetter;
        private readonly IScreenPlacementGetter _screenPlacementGetter;

        public ScreenLoader(
            [NotNull] IScreenDefinitionGetter screenDefinitionGetter,
            [NotNull] IScreenPlacementGetter screenPlacementGetter)
        {
            _screenDefinitionGetter = screenDefinitionGetter;
            _screenPlacementGetter = screenPlacementGetter;
        }

        public void Load<T>(string key, T data)
        {
            IScreenDefinition screenDefinition = _screenDefinitionGetter.Get(key);

            GameObject instance = Instantiate(screenDefinition);

            SetDataIfNeeded(instance, data);
        }

        private GameObject Instantiate([NotNull] IScreenDefinition screenDefinition)
        {
            GameObject prefab = screenDefinition.Prefab;
            Transform placement = _screenPlacementGetter.Get(screenDefinition.PlacementKey).Transform;

            return Object.Instantiate(prefab, placement);
        }

        private static void SetDataIfNeeded<T>([NotNull] GameObject instance, T data)
        {
            if (instance.TryGetComponent(out IDataSettable<T> dataSettable))
            {
                dataSettable.SetData(data);
            }
        }
    }
}