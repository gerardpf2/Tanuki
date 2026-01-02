using System.Collections.Generic;
using Game.Gameplay.Events.Events;
using Game.Gameplay.View.Actions;
using Game.Gameplay.View.Actions.Actions;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolvers.EventResolvers
{
    public class InstantiatePlayerPieceEventResolver : EventResolver<InstantiatePlayerPieceEvent>
    {
        [NotNull] private readonly IActionFactory _actionFactory;
        [NotNull] private readonly IEventResolverFactory _eventResolverFactory;

        public InstantiatePlayerPieceEventResolver(
            [NotNull] IActionFactory actionFactory,
            [NotNull] IEventResolverFactory eventResolverFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);
            ArgumentNullException.ThrowIfNull(eventResolverFactory);

            _actionFactory = actionFactory;
            _eventResolverFactory = eventResolverFactory;
        }

        protected override IEnumerable<IAction> GetActions([NotNull] InstantiatePlayerPieceEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            yield return
                _actionFactory.GetInstantiatePlayerPieceAction(
                    evt.Piece,
                    evt.InstantiatePieceReason,
                    evt.SourceCoordinate
                );

            yield return
                _actionFactory.GetEventResolverAction(
                    _eventResolverFactory.GetInstantiatePlayerPieceGhostEventResolver(),
                    evt.InstantiatePlayerPieceGhostEvent
                );
        }
    }
}