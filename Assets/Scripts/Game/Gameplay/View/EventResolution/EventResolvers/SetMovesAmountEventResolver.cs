using System.Collections.Generic;
using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.View.EventResolution.EventResolvers.Actions;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolution.EventResolvers
{
    public class SetMovesAmountEventResolver : EventResolver<SetMovesAmountEvent>
    {
        [NotNull] private readonly IActionFactory _actionFactory;

        public SetMovesAmountEventResolver([NotNull] IActionFactory actionFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);

            _actionFactory = actionFactory;
        }

        protected override IEnumerable<IAction> GetActions([NotNull] SetMovesAmountEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            yield return _actionFactory.GetSetMovesAmountAction(evt.Amount);
        }
    }
}