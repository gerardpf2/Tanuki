using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure.Unity;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.EventResolution.EventResolvers.Actions
{
    public class ActionGroup : IAction
    {
        [NotNull] private readonly ICoroutineRunner _coroutineRunner;
        [NotNull] private readonly YieldInstruction _waitForSeconds;

        [NotNull, ItemNotNull] private readonly IList<IAction> _actions = new List<IAction>(); // ItemNotNull as long as all Add check for null

        public ActionGroup([NotNull] ICoroutineRunner coroutineRunner, float secondsBetweenActions)
        {
            ArgumentNullException.ThrowIfNull(coroutineRunner);

            _coroutineRunner = coroutineRunner;
            _waitForSeconds = new WaitForSeconds(secondsBetweenActions);
        }

        public void Resolve(Action onComplete)
        {
            _coroutineRunner.Run(ResolveImpl(onComplete));
        }

        public void Add([NotNull] IAction action)
        {
            ArgumentNullException.ThrowIfNull(action);

            _actions.Add(action);
        }

        [NotNull]
        private IEnumerator ResolveImpl(Action onComplete)
        {
            for (int i = 0; i < _actions.Count; ++i)
            {
                IAction action = _actions[i];

                action.Resolve(i == _actions.Count - 1 ? onComplete : null); // TODO: ActionGroupCompletitionHandler

                yield return _waitForSeconds;
            }
        }
    }
}