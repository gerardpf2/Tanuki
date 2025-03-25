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
        [NotNull] private readonly ICoroutineRunner _coroutineRunner;

        [NotNull, ItemNotNull] private readonly ICollection<ITween> _tweensToRun = new List<ITween>();
        [NotNull, ItemNotNull] private readonly List<ITween> _tweens = new();

        private Coroutine _updateCoroutine;

        public TweenRunner([NotNull] ICoroutineRunner coroutineRunner)
        {
            ArgumentNullException.ThrowIfNull(coroutineRunner);

            _coroutineRunner = coroutineRunner;
        }

        public void Run([NotNull] ITween tween)
        {
            ArgumentNullException.ThrowIfNull(tween);

            _tweensToRun.Add(tween);

            _updateCoroutine ??= _coroutineRunner.Run(Update());
        }

        private IEnumerator Update()
        {
            while (_tweensToRun.Count > 0 || _tweens.Count > 0)
            {
                float deltaTimeS = Time.deltaTime;

                _tweens.AddRange(_tweensToRun);

                _tweensToRun.Clear();

                _tweens.RemoveAll(
                    tween =>
                    {
                        tween.Update(deltaTimeS);

                        return tween.State == TweenState.Completed;
                    }
                );

                yield return null;
            }

            _coroutineRunner.Stop(_updateCoroutine);
            _updateCoroutine = null;
        }
    }
}