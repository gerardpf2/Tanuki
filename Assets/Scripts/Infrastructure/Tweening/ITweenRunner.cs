using System;

namespace Infrastructure.Tweening
{
    public interface ITweenRunner
    {
        void Run(ITween tween, Action onRemove = null, Func<bool> keepAliveAfterComplete = null);
    }
}