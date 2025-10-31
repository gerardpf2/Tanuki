using System;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.Unity.Utils
{
    // TODO: Test
    public static class MonoBehaviourUtils
    {
        public static Coroutine WaitForEndOfFrame([NotNull] this MonoBehaviour monoBehaviour, Action action)
        {
            ArgumentNullException.ThrowIfNull(monoBehaviour);

            return monoBehaviour.StartCoroutine(CoroutineUtils.GetWaitForEndOfFrame(action));
        }
    }
}