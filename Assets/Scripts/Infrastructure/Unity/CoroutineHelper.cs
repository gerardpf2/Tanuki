using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Unity
{
    public class CoroutineHelper : ICoroutineHelper
    {
        [NotNull] private static readonly WaitForEndOfFrame WaitForEndOfFrame = new();

        public IEnumerator GetWaitForEndOfFrame(Action action)
        {
            yield return WaitForEndOfFrame;

            action?.Invoke();
        }
    }
}