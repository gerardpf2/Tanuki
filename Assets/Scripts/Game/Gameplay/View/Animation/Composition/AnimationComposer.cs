using Game.Gameplay.View.Animation.Movement;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using Infrastructure.Tweening;
using Infrastructure.Tweening.BuilderHelpers;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Animation.Composition
{
    public class AnimationComposer : ScopeComposer
    {
        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetSingleton<IMovementFactory>(r =>
                    new MovementFactory(
                        r.Resolve<ITransformTweenBuilderHelper>(),
                        r.Resolve<ITweenRunner>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IMovementHelper>(r =>
                    new MovementHelper(
                        r.Resolve<IMovementFactory>()
                    )
                )
            );
        }
    }
}