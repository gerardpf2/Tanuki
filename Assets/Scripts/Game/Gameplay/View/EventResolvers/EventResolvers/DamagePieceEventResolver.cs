using System.Collections.Generic;
using Game.Gameplay.Events.Events;
using Game.Gameplay.View.Actions;
using Game.Gameplay.View.Actions.Actions;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolvers.EventResolvers
{
    public class DamagePieceEventResolver : EventResolver<DamagePieceEvent>
    {
        [NotNull] private readonly IActionFactory _actionFactory;
        [NotNull] private readonly IEventResolverFactory _eventResolverFactory;

        public DamagePieceEventResolver(
            [NotNull] IActionFactory actionFactory,
            [NotNull] IEventResolverFactory eventResolverFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);
            ArgumentNullException.ThrowIfNull(eventResolverFactory);

            _actionFactory = actionFactory;
            _eventResolverFactory = eventResolverFactory;
        }

        protected override IEnumerable<IAction> GetActions([NotNull] DamagePieceEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            yield return
                _actionFactory.GetDamagePieceAction(
                    evt.PieceId,
                    evt.State,
                    evt.DamagePieceReason,
                    evt.Direction
                );

            DestroyPieceEvent destroyPieceEvent = evt.DestroyPieceEvent;

            if (destroyPieceEvent is not null)
            {
                yield return
                    _actionFactory.GetEventResolverAction(
                        _eventResolverFactory.GetDestroyPieceEventResolver(),
                        destroyPieceEvent
                    );
            }
        }
    }
}