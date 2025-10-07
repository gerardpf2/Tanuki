using System.Collections.Generic;
using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.View.EventResolution.EventResolvers.Actions;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolution.EventResolvers
{
    public class SetCameraRowEventResolver : EventResolver<SetCameraRowEvent>
    {
        [NotNull] private readonly IActionFactory _actionFactory;

        public SetCameraRowEventResolver([NotNull] IActionFactory actionFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);

            _actionFactory = actionFactory;
        }

        protected override IEnumerable<IAction> GetActions([NotNull] SetCameraRowEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            yield return _actionFactory.GetSetCameraRowAction(evt.Row);
        }
    }
}