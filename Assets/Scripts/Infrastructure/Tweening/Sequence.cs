using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.Tweening
{
    public class Sequence : TweenBase
    {
        [NotNull, ItemNotNull] private readonly List<ITween> _tweens = new();

        public Sequence(
            float delayS,
            bool autoPlay,
            int repetitions,
            RepetitionType repetitionType,
            Action onIterationComplete,
            Action onComplete,
            [NotNull, ItemNotNull] IEnumerable<ITween> tweens) : base(delayS, autoPlay, repetitions, repetitionType, onIterationComplete, onComplete)
        {
            ArgumentNullException.ThrowIfNull(tweens);

            foreach (ITween tween in tweens)
            {
                ArgumentNullException.ThrowIfNull(tween);

                _tweens.Add(tween);
            }
        }

        public override void Restart(bool withDelay)
        {
            base.Restart(withDelay);

            RestartTweens(withDelay);
        }

        protected override float Refresh(float deltaTimeS, bool backwards)
        {
            backwards ^= Backwards;

            do
            {
                ITween tween = FirstNotCompleted(backwards);

                if (tween is null)
                {
                    break;
                }

                deltaTimeS = tween.Update(deltaTimeS, backwards);
            }
            while (deltaTimeS > 0.0f);

            return deltaTimeS;
        }

        protected override void RestartForNextIteration(bool withDelay)
        {
            base.RestartForNextIteration(withDelay);

            RestartTweens(withDelay);
        }

        private void RestartTweens(bool withDelay)
        {
            foreach (ITween tween in _tweens)
            {
                tween.Restart(withDelay);
            }
        }

        private ITween FirstNotCompleted(bool backwards)
        {
            return backwards ? _tweens.FindLast(IsNotCompleted) : _tweens.Find(IsNotCompleted);
        }

        private static bool IsNotCompleted([NotNull] ITween tween)
        {
            ArgumentNullException.ThrowIfNull(tween);

            return tween.State != TweenState.Completed;
        }
    }
}