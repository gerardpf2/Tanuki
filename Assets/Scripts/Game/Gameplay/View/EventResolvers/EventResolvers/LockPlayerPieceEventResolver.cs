using System.Collections.Generic;
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

        public LockPlayerPieceEventResolver([NotNull] IActionFactory actionFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);

            _actionFactory = actionFactory;
        }

        protected override IEnumerable<IAction> GetActions([NotNull] LockPlayerPieceEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            int rowOffset = evt.LockSourceCoordinate.Row - evt.SourceCoordinate.Row;
            int columnOffset = evt.LockSourceCoordinate.Column - evt.SourceCoordinate.Column;

            yield return _actionFactory.GetMovePlayerPieceAction(rowOffset, columnOffset, MovePieceReason.Lock);

            yield return _actionFactory.GetDestroyPlayerPieceAction(DestroyPieceReason.Lock);

            yield return
                _actionFactory.GetInstantiatePieceAction(
                    evt.Piece,
                    InstantiatePieceReason.Lock,
                    evt.LockSourceCoordinate
                );

            yield return _actionFactory.GetSetMovesAmountAction(evt.MovesAmount);
        }
    }
}