using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.ScreenLoading
{
    public interface IScreenPlacement
    {
        string Key { get; }

        [NotNull]
        Transform Transform { get; }
    }
}