using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.ScreenLoading
{
    public interface IScreenDefinition
    {
        string Key { get; }

        [NotNull]
        GameObject Prefab { get; }

        string PlacementKey { get; }
    }
}