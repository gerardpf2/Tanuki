using System.Collections.Generic;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Infrastructure.ScreenLoading
{
    public class ScreenPlacementContainer : IScreenPlacementAdder, IScreenPlacementGetter
    {
        [NotNull] private readonly IDictionary<string, IScreenPlacement> _screenPlacements = new Dictionary<string, IScreenPlacement>();

        public void Add([NotNull] IScreenPlacement screenPlacement)
        {
            ArgumentNullException.ThrowIfNull(screenPlacement);

            if (screenPlacement.Key is null || !_screenPlacements.TryAdd(screenPlacement.Key, screenPlacement))
            {
                InvalidOperationException.Throw($"Cannot add screen placement with Key: {screenPlacement.Key}");
            }
        }

        public void Remove([NotNull] IScreenPlacement screenPlacement)
        {
            ArgumentNullException.ThrowIfNull(screenPlacement);

            if (screenPlacement.Key is null || !_screenPlacements.Remove(screenPlacement.Key))
            {
                InvalidOperationException.Throw($"Cannot remove screen placement with Key: {screenPlacement.Key}");
            }
        }

        public IScreenPlacement Get([NotNull] string key)
        {
            ArgumentNullException.ThrowIfNull(key);

            if (!_screenPlacements.TryGetValue(key, out IScreenPlacement screenPlacement))
            {
                InvalidOperationException.Throw($"Cannot get screen placement with Key: {key}");
            }

            return screenPlacement;
        }
    }
}