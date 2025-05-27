using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Unity
{
    public interface ICoroutineRunnerHelper
    {
        [NotNull]
        Coroutine RunWaitForEndOfFrame(Action action);
    }
}