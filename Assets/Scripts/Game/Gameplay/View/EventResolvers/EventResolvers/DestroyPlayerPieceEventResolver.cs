using System.Collections.Generic;
using Game.Gameplay.Events.Events;
using Game.Gameplay.View.Actions;
using Game.Gameplay.View.Actions.Actions;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolvers.EventResolvers
{
    public class DestroyPlayerPieceEventResolver : EventResolver<DestroyPlayerPieceEvent>
    {
        [NotNull] private readonly IActionFactory _actionFactory;
        [NotNull] private readonly IEventResolverFactory _eventResolverFactory;

        public DestroyPlayerPieceEventResolver(
            [NotNull] IActionFactory actionFactory,
            [NotNull] IEventResolverFactory eventResolverFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);
            ArgumentNullException.ThrowIfNull(eventResolverFactory);

            _actionFactory = actionFactory;
            _eventResolverFactory = eventResolverFactory;
        }

        protected override IEnumerable<IAction> GetActions([NotNull] DestroyPlayerPieceEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            yield return _actionFactory.GetDestroyPlayerPieceAction(evt.DestroyPieceReason);

            DestroyPlayerPieceGhostEvent destroyPlayerPieceGhostEvent = evt.DestroyPlayerPieceGhostEvent;

            if (destroyPlayerPieceGhostEvent is not null)
            {
                yield return
                    _actionFactory.GetEventResolverAction(
                        _eventResolverFactory.GetDestroyPlayerPieceGhostEventResolver(),
                        destroyPlayerPieceGhostEvent
                    );
            }
        }
    }
}