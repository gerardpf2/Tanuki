using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Unity.Utils
{
    // TODO: Test
    public static class CoroutineUtils
    {
        [NotNull] private static readonly WaitForEndOfFrame WaitForEndOfFrame = new();

        [NotNull]
        public static IEnumerator GetWaitForEndOfFrame(Action action)
        {
            yield return WaitForEndOfFrame;

            action?.Invoke();
        }
    }
}