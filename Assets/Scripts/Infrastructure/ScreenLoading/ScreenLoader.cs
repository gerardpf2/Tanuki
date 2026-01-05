using System.Collections.Generic;
using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.ScreenLoading
{
    // TODO: Test
    public class ScreenLoader : IScreenLoader
    {
        [NotNull] private readonly IScreenDefinitionGetter _screenDefinitionGetter;
        [NotNull] private readonly IScreenPlacementGetter _screenPlacementGetter;
        [NotNull] private readonly IScreenStack _screenStack;

        [NotNull] private readonly IDictionary<string, IScreen> _screens = new Dictionary<string, IScreen>();

        public ScreenLoader(
            [NotNull] IScreenDefinitionGetter screenDefinitionGetter,
            [NotNull] IScreenPlacementGetter screenPlacementGetter,
            [NotNull] IScreenStack screenStack)
        {
            ArgumentNullException.ThrowIfNull(screenDefinitionGetter);
            ArgumentNullException.ThrowIfNull(screenPlacementGetter);
            ArgumentNullException.ThrowIfNull(screenStack);

            _screenDefinitionGetter = screenDefinitionGetter;
            _screenPlacementGetter = screenPlacementGetter;
            _screenStack = screenStack;
        }

        public void Load([NotNull] string key)
        {
            ArgumentNullException.ThrowIfNull(key);

            LoadAndAddRef(key);
        }

        public void Load<T>([NotNull] string key, T data)
        {
            ArgumentNullException.ThrowIfNull(key);

            IScreen screen = LoadAndAddRef(key);

            SetData(screen, data, key);
        }

        public void Unload([NotNull] string key)
        {
            ArgumentNullException.ThrowIfNull(key);

            UnloadAndRemoveRef(key);
        }

        [NotNull]
        private IScreen LoadAndAddRef([NotNull] string key)
        {
            ArgumentNullException.ThrowIfNull(key);

            if (_screens.TryGetValue(key, out IScreen screen))
            {
                InvalidOperationException.ThrowIfNull(screen);
            }
            else
            {
                IScreenDefinition screenDefinition = _screenDefinitionGetter.Get(key);

                screen = Instantiate(screenDefinition);

                _screens.Add(key, screen);
            }

            _screenStack.Push(screen);

            return screen;
        }

        private void UnloadAndRemoveRef([NotNull] string key)
        {
            ArgumentNullException.ThrowIfNull(key);

            if (!_screens.TryGetValue(key, out IScreen screen))
            {
                InvalidOperationException.Throw($"Cannot find screen with Key: {key}");
            }

            InvalidOperationException.ThrowIfNull(screen);

            _screenStack.Remove(screen);

            Object.Destroy(screen.GameObject);

            _screens.Remove(key);
        }

        [NotNull]
        private IScreen Instantiate([NotNull] IScreenDefinition screenDefinition)
        {
            ArgumentNullException.ThrowIfNull(screenDefinition);

            IScreen screen = screenDefinition.Screen;
            GameObject prefab = screen.GameObject;
            Transform placement = _screenPlacementGetter.Get(screen.PlacementKey).Transform;
            GameObject instance = Object.Instantiate(prefab, placement);

            InvalidOperationException.ThrowIfNullWithMessage(
                instance,
                $"Cannot instantiate screen with Key: {screenDefinition.Key}"
            );

            IScreen newScreen = instance.GetComponent<IScreen>();

            InvalidOperationException.ThrowIfNull(newScreen);

            return newScreen;
        }

        private static void SetData<T>([NotNull] IScreen screen, T data, string key)
        {
            ArgumentNullException.ThrowIfNull(screen);

            GameObject gameObject = screen.GameObject;
            IDataSettable<T> dataSettable = gameObject.GetComponent<IDataSettable<T>>();

            InvalidOperationException.ThrowIfNullWithMessage(
                dataSettable,
                $"Cannot set data of Type: {typeof(T)} to screen with Key: {key}"
            );

            dataSettable.SetData(data);
        }
    }
}