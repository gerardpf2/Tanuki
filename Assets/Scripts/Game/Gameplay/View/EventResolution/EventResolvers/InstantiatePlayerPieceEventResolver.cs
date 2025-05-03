using System;
using Game.Gameplay.EventEnqueueing.Events;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.EventResolution.EventResolvers
{
    public class InstantiatePlayerPieceEventResolver : IEventResolver<InstantiatePlayerPieceEvent>
    {
        public void Resolve([NotNull] InstantiatePlayerPieceEvent evt, Action onComplete)
        {
            ArgumentNullException.ThrowIfNull(evt);

            // TODO

            onComplete?.Invoke();
        }
    }
}