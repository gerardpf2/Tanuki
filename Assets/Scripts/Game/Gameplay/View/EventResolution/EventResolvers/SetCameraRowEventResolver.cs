using System;
using Game.Gameplay.EventEnqueueing.Events;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.EventResolution.EventResolvers
{
    public class SetCameraRowEventResolver : IEventResolver<SetCameraRowEvent>
    {
        [NotNull] private readonly IActionFactory _actionFactory;

        public SetCameraRowEventResolver([NotNull] IActionFactory actionFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);

            _actionFactory = actionFactory;
        }

        public void Resolve([NotNull] SetCameraRowEvent evt, Action onComplete)
        {
            ArgumentNullException.ThrowIfNull(evt);

            _actionFactory.GetSetCameraRowAction(evt.Row).Resolve(onComplete);
        }
    }
}