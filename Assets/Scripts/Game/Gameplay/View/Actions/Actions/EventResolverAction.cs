using System;
using Game.Gameplay.Events.Events;
using Game.Gameplay.View.EventResolvers.EventResolvers;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.Actions.Actions
{
    public class EventResolverAction<TEvent> : IAction where TEvent : IEvent
    {
        [NotNull] private readonly IEventResolver<TEvent> _eventResolver;
        private readonly TEvent _evt;

        public EventResolverAction([NotNull] IEventResolver<TEvent> eventResolver, TEvent evt)
        {
            ArgumentNullException.ThrowIfNull(eventResolver);

            _eventResolver = eventResolver;
            _evt = evt;
        }

        public void Resolve(Action onComplete)
        {
            _eventResolver.Resolve(_evt, onComplete);
        }
    }
}