using System;
using UnityEngine;

namespace Infrastructure.Unity
{
    public interface ICoroutineHelper
    {
        Coroutine RunWaitForEndOfFrame(Action action);
    }
}