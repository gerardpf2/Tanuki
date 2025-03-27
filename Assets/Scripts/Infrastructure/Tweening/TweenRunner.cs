using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure.Unity;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.Tweening
{
    public class TweenRunner : ITweenRunner
    {
        private sealed class TweenWrapper
        {
            [NotNull] public readonly ITween Tween;
            public readonly Func<bool> KeepAliveAfterCompleted;

            public TweenWrapper([NotNull] ITween tween, Func<bool> keepAliveAfterCompleted)
            {
                ArgumentNullException.ThrowIfNull(tween);

                Tween = tween;
                KeepAliveAfterCompleted = keepAliveAfterCompleted;
            }
        }

        [NotNull] private readonly ICoroutineRunner _coroutineRunner;

        [NotNull, ItemNotNull] private readonly ICollection<TweenWrapper> _tweenToRunWrappers = new List<TweenWrapper>();
        [NotNull, ItemNotNull] private readonly List<TweenWrapper> _tweenWrappers = new();

        private Coroutine _updateCoroutine;

        public TweenRunner([NotNull] ICoroutineRunner coroutineRunner)
        {
            ArgumentNullException.ThrowIfNull(coroutineRunner);

            _coroutineRunner = coroutineRunner;
        }

        public void Run([NotNull] ITween tween, Func<bool> keepAliveAfterCompleted = null)
        {
            ArgumentNullException.ThrowIfNull(tween);

            _tweenToRunWrappers.Add(new TweenWrapper(tween, keepAliveAfterCompleted));

            _updateCoroutine ??= _coroutineRunner.Run(Update());
        }

        private IEnumerator Update()
        {
            while (_tweenToRunWrappers.Count > 0 || _tweenWrappers.Count > 0)
            {
                float deltaTimeS = Time.deltaTime;

                _tweenWrappers.AddRange(_tweenToRunWrappers);
                _tweenToRunWrappers.Clear();
                _tweenWrappers.RemoveAll(tweenWrapper => Update(tweenWrapper, deltaTimeS));

                yield return null;
            }

            _coroutineRunner.Stop(_updateCoroutine);
            _updateCoroutine = null;
        }

        private static bool Update([NotNull] TweenWrapper tweenWrapper, float deltaTimeS)
        {
            ArgumentNullException.ThrowIfNull(tweenWrapper);

            tweenWrapper.Tween.Update(deltaTimeS);

            bool remove = tweenWrapper.Tween.State == TweenState.Completed;

            if (remove && tweenWrapper.KeepAliveAfterCompleted is not null)
            {
                remove = !tweenWrapper.KeepAliveAfterCompleted();
            }

            return remove;
        }
    }
}