using System;

namespace Infrastructure.Tweening
{
    public interface ITweenRunner
    {
        void Run(ITween tween, Action onRemoved = null, Func<bool> keepAliveAfterCompleted = null);
    }
}