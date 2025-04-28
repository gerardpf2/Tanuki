using System.Collections;
using Game.Gameplay.EventEnqueueing;
using Game.Gameplay.EventEnqueueing.Events;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.EventResolution
{
    public class EventListener : IEventListener
    {
        [NotNull] private readonly ICoroutineRunner _coroutineRunner;
        [NotNull] private readonly IEventDequeuer _eventDequeuer;
        [NotNull] private readonly IEventsResolver _eventsResolver;

        private Coroutine _coroutine;
        private bool _resolving;

        public EventListener(
            [NotNull] ICoroutineRunner coroutineRunner,
            [NotNull] IEventDequeuer eventDequeuer,
            [NotNull] IEventsResolver eventsResolver)
        {
            ArgumentNullException.ThrowIfNull(coroutineRunner);
            ArgumentNullException.ThrowIfNull(eventDequeuer);
            ArgumentNullException.ThrowIfNull(eventsResolver);

            _coroutineRunner = coroutineRunner;
            _eventDequeuer = eventDequeuer;
            _eventsResolver = eventsResolver;
        }

        public void Initialize()
        {
            StartListening();
        }

        private void StartListening()
        {
            StopListening();

            _coroutine = _coroutineRunner.Run(Listen());
        }

        private void StopListening()
        {
            if (_coroutine is null)
            {
                return;
            }

            _coroutineRunner.Stop(_coroutine);
            _coroutine = null;
        }

        private IEnumerator Listen()
        {
            while (true)
            {
                TryResolve();

                yield return null;
            }
        }

        private void TryResolve()
        {
            if (!_eventDequeuer.TryDequeue(out IEvent evt))
            {
                return;
            }

            StopListening();
            Resolve(evt);
        }

        private void Resolve(IEvent evt)
        {
            if (_resolving)
            {
                InvalidOperationException.Throw(); // TODO
            }

            _resolving = true;

            _eventsResolver.Resolve(
                evt,
                () =>
                {
                    _resolving = false;

                    StartListening();
                }
            );
        }
    }
}