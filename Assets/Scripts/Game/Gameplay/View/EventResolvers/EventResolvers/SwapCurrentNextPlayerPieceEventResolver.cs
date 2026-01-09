using System.Collections.Generic;
using Game.Gameplay.Events.Events;
using Game.Gameplay.View.Actions;
using Game.Gameplay.View.Actions.Actions;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolvers.EventResolvers
{
    public class SwapCurrentNextPlayerPieceEventResolver : EventResolver<SwapCurrentNextPlayerPieceEvent>
    {
        [NotNull] private readonly IActionFactory _actionFactory;
        [NotNull] private readonly IEventResolverFactory _eventResolverFactory;

        public SwapCurrentNextPlayerPieceEventResolver(
            [NotNull] IActionFactory actionFactory,
            [NotNull] IEventResolverFactory eventResolverFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);
            ArgumentNullException.ThrowIfNull(eventResolverFactory);

            _actionFactory = actionFactory;
            _eventResolverFactory = eventResolverFactory;
        }

        protected override IEnumerable<IAction> GetActions([NotNull] SwapCurrentNextPlayerPieceEvent evt)
        {
            yield return
                _actionFactory.GetEventResolverAction(
                    _eventResolverFactory.GetDestroyPlayerPieceEventResolver(),
                    evt.DestroyPlayerPieceEvent
                );

            yield return
                _actionFactory.GetEventResolverAction(
                    _eventResolverFactory.GetInstantiatePlayerPieceEventResolver(),
                    evt.InstantiatePlayerPieceEvent
                );
        }
    }
}