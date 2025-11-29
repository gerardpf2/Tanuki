using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.View.Actions;
using Game.Gameplay.View.Actions.Actions;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolvers.EventResolvers
{
    public class LockPlayerPieceEventResolver : EventResolver<LockPlayerPieceEvent>
    {
        [NotNull] private readonly IActionFactory _actionFactory;
        [NotNull] private readonly IEventResolverFactory _eventResolverFactory;

        public LockPlayerPieceEventResolver(
            [NotNull] IActionFactory actionFactory,
            [NotNull] IEventResolverFactory eventResolverFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);
            ArgumentNullException.ThrowIfNull(eventResolverFactory);

            _actionFactory = actionFactory;
            _eventResolverFactory = eventResolverFactory;
        }

        protected override IEnumerable<IAction> GetActions([NotNull] LockPlayerPieceEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            yield return _actionFactory.GetDestroyPlayerPieceGhostAction(DestroyPieceReason.Lock);

            Coordinate sourceCoordinate = evt.SourceCoordinate;
            Coordinate lockSourceCoordinate = evt.InstantiatePieceEvent.SourceCoordinate;

            int rowOffset = lockSourceCoordinate.Row - sourceCoordinate.Row;
            int columnOffset = lockSourceCoordinate.Column - sourceCoordinate.Column;

            yield return _actionFactory.GetMovePlayerPieceAction(rowOffset, columnOffset, MovePieceReason.Lock);

            yield return _actionFactory.GetDestroyPlayerPieceAction(DestroyPieceReason.Lock);

            yield return
                _actionFactory.GetEventResolverAction(
                    _eventResolverFactory.GetInstantiatePieceEventResolver(),
                    evt.InstantiatePieceEvent
                );

            yield return _actionFactory.GetSetMovesAmountAction(evt.MovesAmount);
        }
    }
}