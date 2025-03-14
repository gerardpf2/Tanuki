using Infrastructure.ModelViewViewModel;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.ScreenLoading
{
    public class ScreenLoader : IScreenLoader
    {
        private readonly IScreenDefinitionGetter _screenDefinitionGetter;
        private readonly IScreenPlacementGetter _screenPlacementGetter;
        private readonly Transform _rootTransform;

        public ScreenLoader(
            [NotNull] IScreenDefinitionGetter screenDefinitionGetter,
            [NotNull] IScreenPlacementGetter screenPlacementGetter,
            [NotNull] Transform rootTransform)
        {
            _screenDefinitionGetter = screenDefinitionGetter;
            _screenPlacementGetter = screenPlacementGetter;
            _rootTransform = rootTransform;
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

            Transform placement = screenDefinition.UsePlacement ?
                _screenPlacementGetter.Get(screenDefinition.PlacementKey).Transform :
                _rootTransform;

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