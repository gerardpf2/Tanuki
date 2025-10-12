using System.Collections.Generic;
using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.View.EventResolution.EventResolvers.Actions;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolution.EventResolvers
{
    public class SetGoalCurrentAmountEventResolver : EventResolver<SetGoalCurrentAmountEvent>
    {
        [NotNull] private readonly IActionFactory _actionFactory;

        public SetGoalCurrentAmountEventResolver([NotNull] IActionFactory actionFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);

            _actionFactory = actionFactory;
        }

        protected override IEnumerable<IAction> GetActions([NotNull] SetGoalCurrentAmountEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            yield return _actionFactory.GetSetGoalCurrentAmountAction(evt.PieceType, evt.CurrentAmount, evt.Coordinate);
        }
    }
}