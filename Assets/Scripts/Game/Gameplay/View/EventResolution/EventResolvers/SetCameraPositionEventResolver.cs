using System;
using Game.Gameplay.EventEnqueueing.Events;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.EventResolution.EventResolvers
{
    public class SetCameraPositionEventResolver : IEventResolver<SetCameraPositionEvent>
    {
        [NotNull] private readonly IActionFactory _actionFactory;

        public SetCameraPositionEventResolver([NotNull] IActionFactory actionFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);

            _actionFactory = actionFactory;
        }

        public void Resolve([NotNull] SetCameraPositionEvent evt, Action onComplete)
        {
            ArgumentNullException.ThrowIfNull(evt);

            _actionFactory.GetSetCameraPositionAction(evt.TopRow, evt.BottomRow).Resolve(onComplete);
        }
    }
}