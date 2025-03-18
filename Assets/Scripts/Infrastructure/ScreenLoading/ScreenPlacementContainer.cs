using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.ScreenLoading
{
    public class ScreenPlacementContainer : IScreenPlacementAdder, IScreenPlacementGetter
    {
        [NotNull] private readonly IDictionary<string, IScreenPlacement> _screenPlacements = new Dictionary<string, IScreenPlacement>();

        public void Add([NotNull] IScreenPlacement screenPlacement)
        {
            ArgumentNullException.ThrowIfNull(screenPlacement);

            if (screenPlacement.Key is not null && _screenPlacements.TryAdd(screenPlacement.Key, screenPlacement))
            {
                return;
            }

            throw new InvalidOperationException($"Cannot add screen placement with Key: {screenPlacement.Key}");
        }

        public void Remove([NotNull] IScreenPlacement screenPlacement)
        {
            ArgumentNullException.ThrowIfNull(screenPlacement);

            if (screenPlacement.Key is not null && _screenPlacements.Remove(screenPlacement.Key))
            {
                return;
            }

            throw new InvalidOperationException($"Cannot remove screen placement with Key: {screenPlacement.Key}");
        }

        public IScreenPlacement Get([NotNull] string key)
        {
            ArgumentNullException.ThrowIfNull(key);

            if (_screenPlacements.TryGetValue(key, out IScreenPlacement screenPlacement))
            {
                return screenPlacement;
            }

            throw new InvalidOperationException($"Cannot get screen placement with Key: {key}");
        }
    }
}