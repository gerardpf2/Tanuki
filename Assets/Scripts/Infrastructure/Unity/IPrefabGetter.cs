using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Unity
{
    public interface IPrefabGetter
    {
        [NotNull]
        GameObject Get(string key);
    }
}