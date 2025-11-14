using Game.Gameplay.Moves;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Moves.Composition
{
    public class MovesComposer : ScopeComposer
    {
        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(ruleFactory.GetSingleton<IMoves>(_ => new Gameplay.Moves.Moves()), MovesComposerKeys.Moves.View);

            ruleAdder.Add(
                ruleFactory.GetSingleton<IMovesView>(r =>
                    new MovesView(
                        r.Resolve<IMoves>(),
                        r.Resolve<IMoves>(MovesComposerKeys.Moves.View)
                    )
                )
            );
        }

        protected override void AddSharedRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddSharedRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetInject<MovesViewModel>((r, vm) =>
                    vm.Inject(
                        r.Resolve<IMovesView>()
                    )
                )
            );
        }
    }
}