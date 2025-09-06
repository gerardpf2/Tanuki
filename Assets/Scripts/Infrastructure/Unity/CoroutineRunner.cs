using System.Collections;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Unity
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        public Coroutine Run([NotNull] IEnumerator enumerator)
        {
            ArgumentNullException.ThrowIfNull(enumerator);

            Coroutine coroutine = StartCoroutine(enumerator);

            InvalidOperationException.ThrowIfNull(coroutine);

            return coroutine;
        }

        public void Stop([NotNull] Coroutine coroutine)
        {
            ArgumentNullException.ThrowIfNull(coroutine);

            StopCoroutine(coroutine);
        }
    }
}