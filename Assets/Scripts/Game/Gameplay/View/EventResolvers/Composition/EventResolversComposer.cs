using Game.Gameplay.Events;
using Game.Gameplay.Phases;
using Game.Gameplay.View.Actions;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolvers.Composition
{
    public class EventResolversComposer : ScopeComposer
    {
        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetSingleton<IEventResolverFactory>(r =>
                    new EventResolverFactory(
                        r.Resolve<IActionFactory>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IEventsResolver>(r =>
                    new EventsResolver(
                        r.Resolve<IEventEnqueuer>(),
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<IEventsResolverSingle>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IEventsResolverSingle>(r =>
                    new EventsResolverSingle(
                        r.Resolve<IEventResolverFactory>()
                    )
                )
            );
        }
    }
}