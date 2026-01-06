using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.ScreenLoading
{
    public interface IScreen
    {
        string Key { get; }

        string PlacementKey { get; }

        [NotNull]
        GameObject GameObject { get; }

        void OnFocus(bool focused);
    }
}