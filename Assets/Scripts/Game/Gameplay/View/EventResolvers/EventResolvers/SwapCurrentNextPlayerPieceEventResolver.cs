using System.Collections.Generic;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Events.Reasons;
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
            // TODO: Check if destroy player piece and piece ghost need their own resolver

            yield return _actionFactory.GetDestroyPlayerPieceGhostAction(DestroyPieceReason.Lock);

            yield return _actionFactory.GetDestroyPlayerPieceAction(DestroyPieceReason.Lock);

            yield return
                _actionFactory.GetEventResolverAction(
                    _eventResolverFactory.GetInstantiatePieceEventResolver(),
                    evt.InstantiatePieceEvent
                );
        }
    }
}