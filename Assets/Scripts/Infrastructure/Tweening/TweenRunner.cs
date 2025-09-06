using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure.System;
using Infrastructure.Unity;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;

namespace Infrastructure.Tweening
{
    public class TweenRunner : ITweenRunner
    {
        private sealed class TweenWrapper
        {
            [NotNull] public readonly ITween Tween;
            public readonly Action OnRemove;
            public readonly Func<bool> KeepAliveAfterComplete;

            public TweenWrapper([NotNull] ITween tween, Action onRemove, Func<bool> keepAliveAfterComplete)
            {
                ArgumentNullException.ThrowIfNull(tween);

                Tween = tween;
                OnRemove = onRemove;
                KeepAliveAfterComplete = keepAliveAfterComplete;
            }
        }

        [NotNull] private readonly ICoroutineRunner _coroutineRunner;
        [NotNull] private readonly IDeltaTimeGetter _deltaTimeGetter;

        [NotNull, ItemNotNull] private readonly ICollection<TweenWrapper> _tweenToRunWrappers = new List<TweenWrapper>();
        [NotNull, ItemNotNull] private readonly List<TweenWrapper> _tweenWrappers = new();

        private Coroutine _updateCoroutine;
        private bool _running;

        public TweenRunner([NotNull] ICoroutineRunner coroutineRunner, [NotNull] IDeltaTimeGetter deltaTimeGetter)
        {
            ArgumentNullException.ThrowIfNull(coroutineRunner);
            ArgumentNullException.ThrowIfNull(deltaTimeGetter);

            _coroutineRunner = coroutineRunner;
            _deltaTimeGetter = deltaTimeGetter;
        }

        public void Run([NotNull] ITween tween, Action onRemove = null, Func<bool> keepAliveAfterComplete = null)
        {
            ArgumentNullException.ThrowIfNull(tween);

            _tweenToRunWrappers.Add(new TweenWrapper(tween, onRemove, keepAliveAfterComplete));

            if (_running)
            {
                return;
            }

            _running = true;

            _updateCoroutine = _coroutineRunner.Run(Update());
        }

        private IEnumerator Update()
        {
            while (_tweenToRunWrappers.Count > 0 || _tweenWrappers.Count > 0)
            {
                float deltaTimeS = _deltaTimeGetter.Get();

                _tweenWrappers.AddRange(_tweenToRunWrappers);
                _tweenToRunWrappers.Clear();
                _tweenWrappers.RemoveAll(tweenWrapper => Update(tweenWrapper, deltaTimeS));

                yield return null;
            }

            _running = false;

            _coroutineRunner.Stop(_updateCoroutine);
            _updateCoroutine = null;
        }

        private static bool Update(
            [NotNull] TweenWrapper tweenWrapper,
            [Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f)] float deltaTimeS)
        {
            ArgumentNullException.ThrowIfNull(tweenWrapper);
            ArgumentOutOfRangeException.ThrowIfNot(deltaTimeS, ComparisonOperator.GreaterThanOrEqualTo, 0.0f);

            tweenWrapper.Tween.Update(deltaTimeS);

            bool remove = tweenWrapper.Tween.State is TweenState.Complete;

            if (remove && tweenWrapper.KeepAliveAfterComplete is not null)
            {
                remove = !tweenWrapper.KeepAliveAfterComplete();
            }

            if (remove)
            {
                tweenWrapper.OnRemove?.Invoke();
            }

            return remove;
        }
    }
}