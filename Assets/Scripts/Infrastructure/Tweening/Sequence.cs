using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

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
            float remainingDeltaTimeS = deltaTimeS;

            do
            {
                ITween tween = FirstNotCompleted(backwards);

                if (tween is null)
                {
                    break;
                }

                float updatedRemainingDeltaTimeS = tween.Update(remainingDeltaTimeS, backwards);

                if (updatedRemainingDeltaTimeS > remainingDeltaTimeS || updatedRemainingDeltaTimeS < 0.0f)
                {
                    InvalidOperationException.Throw(); // TODO
                }

                remainingDeltaTimeS = updatedRemainingDeltaTimeS;
            }
            while (remainingDeltaTimeS > 0.0f);

            return remainingDeltaTimeS;
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