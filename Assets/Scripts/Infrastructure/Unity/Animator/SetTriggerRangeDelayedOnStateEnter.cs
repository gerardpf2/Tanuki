using System.Collections;
using Infrastructure.System;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Unity.Animator
{
    public class SetTriggerRangeDelayedOnStateEnter : StateMachineBehaviour
    {
        [SerializeField] private string _triggerName;
        [SerializeField, Min(0.0f)] private float _minDelayS;
        [SerializeField, Min(0.0f)] private float _maxDelayS;

        private CoroutineRunner _coroutineRunner; // Not using the interface is intended. Check StopCoroutineIfNeeded comment
        private Coroutine _coroutine;

        private void OnDestroy()
        {
            StopCoroutineIfNeeded();
        }

        public override void OnStateEnter(
            [NotNull] UnityEngine.Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            ArgumentNullException.ThrowIfNull(animator);

            base.OnStateEnter(animator, stateInfo, layerIndex);

            FindCoroutineRunnerIfNeeded(animator);
            StartCoroutine(animator);
        }

        public override void OnStateExit(UnityEngine.Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);

            StopCoroutineIfNeeded();
        }

        private void StartCoroutine([NotNull] UnityEngine.Animator animator)
        {
            ArgumentNullException.ThrowIfNull(animator);
            InvalidOperationException.ThrowIfNull(_coroutineRunner);

            StopCoroutineIfNeeded();

            _coroutine = _coroutineRunner.Run(SetTrigger(animator));
        }

        private void StopCoroutineIfNeeded()
        {
            if (!_coroutineRunner || // Not using == / != / is null / is not null because these throw an exception when it gets destroyed
                _coroutine is null)
            {
                return;
            }

            _coroutineRunner.Stop(_coroutine);
            _coroutine = null;
        }

        private void FindCoroutineRunnerIfNeeded([NotNull] Component component)
        {
            ArgumentNullException.ThrowIfNull(component);

            _coroutineRunner ??= component.GetComponent<CoroutineRunner>();
        }

        [NotNull]
        private IEnumerator SetTrigger([NotNull] UnityEngine.Animator animator)
        {
            ArgumentNullException.ThrowIfNull(animator);
            InvalidOperationException.ThrowIfNot(_minDelayS, ComparisonOperator.LessThanOrEqualTo, _maxDelayS);

            float delay = Random.Range(_minDelayS, _maxDelayS);

            yield return new WaitForSeconds(delay);

            animator.SetTrigger(_triggerName);

            _coroutine = null;
        }
    }
}