using Infrastructure.Tweening.EasingFunctions;
using JetBrains.Annotations;

namespace Infrastructure.Tweening
{
    public interface IEasingFunctionFactory
    {
        [NotNull]
        IEasingFunction GetInQuad();

        [NotNull]
        IEasingFunction GetOutQuad();

        [NotNull]
        IEasingFunction GetInOutQuad();

        [NotNull]
        IEasingFunction GetLinear();

        [NotNull]
        IEasingFunction GetInSine();

        [NotNull]
        IEasingFunction GetOutSine();

        [NotNull]
        IEasingFunction GetInOutSine();

        [NotNull]
        IEasingFunction GetInCubic();

        [NotNull]
        IEasingFunction GetOutCubic();

        [NotNull]
        IEasingFunction GetInOutCubic();

        [NotNull]
        IEasingFunction GetInCirc();

        [NotNull]
        IEasingFunction GetOutCirc();

        [NotNull]
        IEasingFunction GetInOutCirc();

        [NotNull]
        IEasingFunction GetInElastic();

        [NotNull]
        IEasingFunction GetOutElastic();

        [NotNull]
        IEasingFunction GetInOutElastic();

        [NotNull]
        IEasingFunction GetInBack();

        [NotNull]
        IEasingFunction GetOutBack();

        [NotNull]
        IEasingFunction GetInOutBack();

        [NotNull]
        IEasingFunction GetInBounce();

        [NotNull]
        IEasingFunction GetOutBounce();

        [NotNull]
        IEasingFunction GetInOutBounce();
    }
}