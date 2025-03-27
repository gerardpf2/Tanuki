using System;

namespace Infrastructure.Tweening
{
    public interface ITweenRunner
    {
        void Run(ITween tween, Func<bool> keepAliveAfterCompleted = null);
    }
}