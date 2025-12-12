using Infrastructure.Tweening.EasingFunctions;
using JetBrains.Annotations;

namespace Infrastructure.Tweening
{
    public interface IEasingFunctionGetter
    {
        [NotNull]
        IEasingFunction Get(EasingType easingType);

        [NotNull]
        IEasingFunction GetComplementary(EasingType easingType);
    }
}