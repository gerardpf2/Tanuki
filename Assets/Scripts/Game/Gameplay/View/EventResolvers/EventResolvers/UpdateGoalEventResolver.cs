using System.Collections.Generic;
using Game.Gameplay.Events.Events;
using Game.Gameplay.View.Actions;
using Game.Gameplay.View.Actions.Actions;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolvers.EventResolvers
{
    public class UpdateGoalEventResolver : EventResolver<UpdateGoalEvent>
    {
        [NotNull] private readonly IActionFactory _actionFactory;

        public UpdateGoalEventResolver([NotNull] IActionFactory actionFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);

            _actionFactory = actionFactory;
        }

        protected override IEnumerable<IAction> GetActions([NotNull] UpdateGoalEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            yield return _actionFactory.GetSetGoalCurrentAmountAction(evt.PieceType, evt.CurrentAmount, evt.Coordinate);
        }
    }
}