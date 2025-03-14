using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.ScreenLoading
{
    public interface IScreenDefinition
    {
        string Key { get; }

        [NotNull]
        GameObject Prefab { get; }

        bool UsePlacement { get; }

        string PlacementKey { get; }
    }
}