using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Unity
{
    public interface ICoroutineRunner
    {
        [NotNull]
        Coroutine Run(IEnumerator enumerator);

        void Stop(Coroutine coroutine);
    }
}