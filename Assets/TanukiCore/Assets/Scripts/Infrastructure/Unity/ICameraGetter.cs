using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Unity
{
    public interface ICameraGetter
    {
        [NotNull]
        Camera GetMain();
    }
}