using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using Infrastructure.Tweening.TweenBuilderHelpers;
using Infrastructure.Unity;
using JetBrains.Annotations;

namespace Infrastructure.Tweening.Composition
{
    public class TweeningComposer : ScopeComposer
    {
        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetSingleton<ITransformTweenBuilderHelper>(r =>
                    new TransformTweenBuilderHelper(
                        r.Resolve<ITweenBuilderFactory>()
                    )
                )
            );

            ruleAdder.Add(ruleFactory.GetSingleton<IEasingFunctionGetter>(_ => new EasingFunctionGetter()));

            ruleAdder.Add(
                ruleFactory.GetSingleton<ITweenBuilderFactory>(r =>
                    new TweenBuilderFactory(
                        r.Resolve<IEasingFunctionGetter>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<ITweenRunner>(r =>
                    new TweenRunner(
                        r.Resolve<ICoroutineRunner>()
                    )
                )
            );
        }
    }
}