using System;
using JetBrains.Annotations;

namespace Infrastructure.Tweening
{
    public interface IEasingFunctionFactory
    {
        [NotNull]
        Func<float, float> GetInQuad();

        [NotNull]
        Func<float, float> GetOutQuad();

        [NotNull]
        Func<float, float> GetInOutQuad();

        [NotNull]
        Func<float, float> GetLinear();

        [NotNull]
        Func<float, float> GetInSine();

        [NotNull]
        Func<float, float> GetOutSine();

        [NotNull]
        Func<float, float> GetInOutSine();

        [NotNull]
        Func<float, float> GetInCubic();

        [NotNull]
        Func<float, float> GetOutCubic();

        [NotNull]
        Func<float, float> GetInOutCubic();

        [NotNull]
        Func<float, float> GetInCirc();

        [NotNull]
        Func<float, float> GetOutCirc();

        [NotNull]
        Func<float, float> GetInOutCirc();

        [NotNull]
        Func<float, float> GetInElastic();

        [NotNull]
        Func<float, float> GetOutElastic();

        [NotNull]
        Func<float, float> GetInOutElastic();

        [NotNull]
        Func<float, float> GetInBack();

        [NotNull]
        Func<float, float> GetOutBack();

        [NotNull]
        Func<float, float> GetInOutBack();

        [NotNull]
        Func<float, float> GetInBounce();

        [NotNull]
        Func<float, float> GetOutBounce();

        [NotNull]
        Func<float, float> GetInOutBounce();
    }
}