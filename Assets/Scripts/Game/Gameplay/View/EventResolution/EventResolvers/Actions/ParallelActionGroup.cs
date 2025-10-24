using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure.System;
using Infrastructure.Unity;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.EventResolution.EventResolvers.Actions
{
    public class ParallelActionGroup : IAction
    {
        [NotNull] private readonly ICoroutineRunner _coroutineRunner;
        private readonly float _secondsBetweenActions;
        [NotNull] private readonly YieldInstruction _waitForSeconds;

        [NotNull, ItemNotNull] private readonly ICollection<IAction> _actions = new List<IAction>(); // ItemNotNull as long as all Add check for null

        public ParallelActionGroup([NotNull] ICoroutineRunner coroutineRunner, float secondsBetweenActions)
        {
            ArgumentNullException.ThrowIfNull(coroutineRunner);

            _coroutineRunner = coroutineRunner;
            _secondsBetweenActions = secondsBetweenActions;
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
            ActionGroupCompletionHandler actionGroupCompletionHandler = new(_actions.Count, onComplete);

            foreach (IAction action in _actions)
            {
                action.Resolve(actionGroupCompletionHandler.RegisterCompleted);

                if (_secondsBetweenActions > 0.0f)
                {
                    yield return _waitForSeconds;
                }
            }
        }
    }
}