using System.Collections.Generic;
using Game.Gameplay.Events.Events;
using Game.Gameplay.View.Actions;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolvers.EventResolvers
{
    public class DestroyPieceEventResolver : EventResolver<DestroyPieceEvent>
    {
        [NotNull] private readonly IActionFactory _actionFactory;

        public DestroyPieceEventResolver([NotNull] IActionFactory actionFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);

            _actionFactory = actionFactory;
        }

        protected override IEnumerable<IAction> GetActions([NotNull] DestroyPieceEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            yield return _actionFactory.GetDestroyPieceAction(evt.PieceId, evt.DestroyPieceReason);

            DestroyPieceEvent.GoalCurrentAmountUpdatedData goalData = evt.GoalData;

            if (goalData is not null)
            {
                yield return
                    _actionFactory.GetSetGoalCurrentAmountAction(
                        goalData.PieceType,
                        goalData.CurrentAmount,
                        goalData.Coordinate
                    );
            }
        }
    }
}