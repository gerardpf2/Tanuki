using System.Collections;
using UnityEngine;

namespace Infrastructure.Unity
{
    public interface ICoroutineRunner
    {
        Coroutine Run(IEnumerator enumerator);

        void Stop(Coroutine coroutine);
    }
}