using Game.Gameplay.Phases;
using Game.Gameplay.View.EventResolvers;
using Game.Gameplay.View.Player;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Input.Composition
{
    public class InputComposer : ScopeComposer
    {
        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetSingleton<IInputHandler>(r =>
                    new InputHandler(
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<IEventsResolver>(),
                        r.Resolve<IInputListener>(),
                        r.Resolve<IPlayerPieceView>(),
                        r.Resolve<IScreenPropertiesGetter>()
                    )
                )
            );

            ruleAdder.Add(ruleFactory.GetSingleton(_ => new InputEventsHandler()));
            ruleAdder.Add(ruleFactory.GetTo<IInputListener, InputEventsHandler>());
            ruleAdder.Add(ruleFactory.GetTo<IInputNotifier, InputEventsHandler>());
        }
    }
}