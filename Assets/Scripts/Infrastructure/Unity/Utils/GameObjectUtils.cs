using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Unity.Utils
{
    // TODO: Test
    public static class GameObjectUtils
    {
        [NotNull]
        public static GameObject WithParent([NotNull] this GameObject gameObject, Transform parent)
        {
            ArgumentNullException.ThrowIfNull(gameObject);

            gameObject.transform.SetParent(parent);

            return gameObject;
        }
    }
}