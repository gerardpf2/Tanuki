using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using Infrastructure.Tweening.TweenBuilders;
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

            ruleAdder.Add(ruleFactory.GetSingleton<ISequenceBuilder>(_ => new SequenceBuilder()));

            ruleAdder.Add(
                ruleFactory.GetSingleton(r =>
                    new TweenBuilderFloat(
                        r.Resolve<IEasingFunctionGetter>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton(r =>
                    new TweenBuilderVector3(
                        r.Resolve<IEasingFunctionGetter>()
                    )
                )
            );

            ruleAdder.Add(ruleFactory.GetSingleton<IEasingFunctionGetter>(_ => new EasingFunctionGetter()));

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