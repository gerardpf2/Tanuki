using System.Collections.Generic;
using Game.Gameplay.Events.Events;
using Game.Gameplay.View.EventResolution.EventResolvers.Actions;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolution.EventResolvers
{
    public class MoveCameraEventResolver : EventResolver<MoveCameraEvent>
    {
        [NotNull] private readonly IActionFactory _actionFactory;

        public MoveCameraEventResolver([NotNull] IActionFactory actionFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);

            _actionFactory = actionFactory;
        }

        protected override IEnumerable<IAction> GetActions([NotNull] MoveCameraEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            yield return _actionFactory.GetMoveCameraAction(evt.RowOffset);
        }
    }
}