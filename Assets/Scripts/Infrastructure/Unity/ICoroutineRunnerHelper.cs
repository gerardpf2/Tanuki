using System;
using UnityEngine;

namespace Infrastructure.Unity
{
    public interface ICoroutineRunnerHelper
    {
        Coroutine RunWaitForEndOfFrame(Action action);
    }
}