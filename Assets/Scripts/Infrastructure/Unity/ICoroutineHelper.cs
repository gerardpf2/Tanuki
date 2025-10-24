using System;
using System.Collections;
using JetBrains.Annotations;

namespace Infrastructure.Unity
{
    public interface ICoroutineHelper
    {
        [NotNull]
        IEnumerator GetWaitForEndOfFrame(Action action);
    }
}