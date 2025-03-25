using System.Collections;
using System.Collections.Generic;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Tweening
{
    public class TweenRunner : ITweenRunner
    {
        private sealed class TweenWrapper
        {
            [NotNull] public readonly ITween Tween;
            public readonly bool KeepAlive;

            public TweenWrapper([NotNull] ITween tween, bool keepAlive)
            {
                ArgumentNullException.ThrowIfNull(tween);

                Tween = tween;
                KeepAlive = keepAlive;
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

        public void Run([NotNull] ITween tween, bool keepAlive = false)
        {
            ArgumentNullException.ThrowIfNull(tween);

            _tweenToRunWrappers.Add(new TweenWrapper(tween, keepAlive));

            _updateCoroutine ??= _coroutineRunner.Run(Update());
        }

        private IEnumerator Update()
        {
            while (_tweenToRunWrappers.Count > 0 || _tweenWrappers.Count > 0)
            {
                float deltaTimeS = Time.deltaTime;

                _tweenWrappers.AddRange(_tweenToRunWrappers);

                _tweenToRunWrappers.Clear();

                _tweenWrappers.RemoveAll(
                    tweenWrapper =>
                    {
                        tweenWrapper.Tween.Update(deltaTimeS);

                        return tweenWrapper.Tween.State == TweenState.Completed && !tweenWrapper.KeepAlive;
                    }
                );

                yield return null;
            }

            _coroutineRunner.Stop(_updateCoroutine);
            _updateCoroutine = null;
        }
    }
}