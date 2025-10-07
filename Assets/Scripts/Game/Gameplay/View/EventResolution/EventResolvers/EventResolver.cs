using System;
using System.Collections.Generic;
using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.View.EventResolution.EventResolvers.Actions;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.EventResolution.EventResolvers
{
    public abstract class EventResolver<T> : IEventResolver<T> where T : IEvent
    {
        public void Resolve(T evt, Action onComplete)
        {
            IEnumerator<IAction> actions = GetActions(evt).GetEnumerator();

            ResolveActions(actions, OnComplete);

            return;

            void OnComplete()
            {
                actions.Dispose();

                onComplete?.Invoke();
            }
        }

        [NotNull, ItemNotNull]
        protected abstract IEnumerable<IAction> GetActions(T evt);

        private static void ResolveActions([NotNull] IEnumerator<IAction> actions, Action onComplete)
        {
            ArgumentNullException.ThrowIfNull(actions);

            bool hasNext = actions.MoveNext();

            if (!hasNext)
            {
                onComplete?.Invoke();

                return;
            }

            IAction action = actions.Current;

            InvalidOperationException.ThrowIfNull(action);

            action.Resolve(OnActionComplete);

            return;

            void OnActionComplete()
            {
                ResolveActions(actions, onComplete);
            }
        }
    }
}