using System;

namespace Infrastructure.Tweening
{
    public interface ITweenRunner
    {
        void Run(ITweenBase tween, Action onRemove = null, Func<bool> keepAliveAfterComplete = null);
    }
}