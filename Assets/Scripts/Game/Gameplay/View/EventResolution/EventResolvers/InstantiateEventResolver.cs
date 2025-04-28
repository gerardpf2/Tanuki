using Game.Gameplay.EventEnqueueing.Events;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolution.EventResolvers
{
    public class InstantiateEventResolver : IEventResolver<InstantiateEvent>
    {
        public void Resolve([NotNull] InstantiateEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            // TODO
        }
    }
}