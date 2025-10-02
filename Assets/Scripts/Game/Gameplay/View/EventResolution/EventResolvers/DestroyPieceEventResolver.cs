using System;
using Game.Gameplay.EventEnqueueing.Events;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.EventResolution.EventResolvers
{
    public class DestroyPieceEventResolver : IEventResolver<DestroyPieceEvent>
    {
        [NotNull] private readonly IActionFactory _actionFactory;

        public DestroyPieceEventResolver([NotNull] IActionFactory actionFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);

            _actionFactory = actionFactory;
        }

        public void Resolve([NotNull] DestroyPieceEvent evt, Action onComplete)
        {
            ArgumentNullException.ThrowIfNull(evt);

            _actionFactory.GetDestroyPieceAction(evt.Id, evt.DestroyPieceReason).Resolve(onComplete);
        }
    }
}