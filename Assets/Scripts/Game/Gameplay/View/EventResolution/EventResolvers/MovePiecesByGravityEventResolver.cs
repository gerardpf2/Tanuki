using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.EventEnqueueing.Events.Reasons;
using Game.Gameplay.View.EventResolution.EventResolvers.Actions;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolution.EventResolvers
{
    public class MovePiecesByGravityEventResolver : EventResolver<MovePiecesByGravityEvent>
    {
        private const float SecondsBetweenActions = 0.1f;

        [NotNull] private readonly IActionFactory _actionFactory;

        public MovePiecesByGravityEventResolver([NotNull] IActionFactory actionFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);

            _actionFactory = actionFactory;
        }

        protected override IEnumerable<IAction> GetActions([NotNull] MovePiecesByGravityEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            IEnumerable<IAction> actions = evt.PiecesMovementsData.Select(GetMovePieceAction);

            yield return _actionFactory.GetParallelActionGroup(actions, SecondsBetweenActions);

            yield break;

            [NotNull]
            IAction GetMovePieceAction([NotNull] MovePiecesByGravityEvent.PieceMovementData pieceMovementData)
            {
                ArgumentNullException.ThrowIfNull(pieceMovementData);

                return
                    _actionFactory.GetMovePieceAction(
                        pieceMovementData.PieceId,
                        pieceMovementData.RowOffset,
                        pieceMovementData.ColumnOffset,
                        MovePieceReason.Gravity
                    );
            }
        }
    }
}