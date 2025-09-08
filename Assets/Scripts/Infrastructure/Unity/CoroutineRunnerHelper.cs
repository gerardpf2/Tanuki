using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.Unity
{
    public class CoroutineRunnerHelper : ICoroutineRunnerHelper
    {
        [NotNull] private readonly ICoroutineRunner _coroutineRunner;

        [NotNull] private static readonly WaitForEndOfFrame WaitForEndOfFrame = new();

        public CoroutineRunnerHelper([NotNull] ICoroutineRunner coroutineRunner)
        {
            ArgumentNullException.ThrowIfNull(coroutineRunner);

            _coroutineRunner = coroutineRunner;
        }

        public Coroutine RunWaitForEndOfFrame(Action action)
        {
            return _coroutineRunner.Run(GetWaitForEndOfFrame(action));
        }

        private static IEnumerator GetWaitForEndOfFrame(Action action)
        {
            yield return WaitForEndOfFrame;

            action?.Invoke();
        }
    }
}