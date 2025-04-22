using System;
using System.Collections.Generic;
using Infrastructure.System;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;

namespace Infrastructure.Tweening
{
    public class SequenceAsync : SequenceBase
    {
        public SequenceAsync(
            bool autoPlay,
            [Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f)] float delayBeforeS,
            [Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f)] float delayAfterS,
            int repetitions,
            RepetitionType repetitionType,
            DelayManagement delayManagementRepetition,
            DelayManagement delayManagementRestart,
            Action onStartIteration,
            Action onStartPlay,
            Action onEndPlay,
            Action onEndIteration,
            Action onPause,
            Action onResume,
            Action onRestart,
            Action onComplete,
            [NotNull] [ItemNotNull] IEnumerable<ITween> tweens) : base(autoPlay, delayBeforeS, delayAfterS, repetitions, repetitionType, delayManagementRepetition, delayManagementRestart, onStartIteration, onStartPlay, onEndPlay, onEndIteration, onPause, onResume, onRestart, onComplete, tweens) { }

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f), Is(ComparisonOperator.LessThanOrEqualTo, "deltaTimeS")]
        protected override float Play(
            [Is(ComparisonOperator.GreaterThan, 0.0f)] float deltaTimeS,
            bool backwards,
            IReadOnlyList<ITween> tweens)
        {
            ArgumentOutOfRangeException.ThrowIfNot(deltaTimeS, ComparisonOperator.GreaterThan, 0.0f);
            ArgumentNullException.ThrowIfNull(tweens);

            backwards ^= Backwards;

            float minRemainingDeltaTimeS = deltaTimeS;

            foreach (ITween tween in tweens)
            {
                ArgumentNullException.ThrowIfNull(tween);

                float remainingDeltaTimeS = tween.Update(deltaTimeS, backwards);

                minRemainingDeltaTimeS = Math.Min(minRemainingDeltaTimeS, remainingDeltaTimeS);
            }

            return minRemainingDeltaTimeS;
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj)) // Already checks null / ReferenceEquals
            {
                return false;
            }

            return obj is SequenceAsync;
        }

        public override int GetHashCode()
        {
            return
                HashCode.Combine(
                    base.GetHashCode(),
                    typeof(SequenceAsync) // Not sure if it is needed, but since SequenceAsync has no new fields, this should differentiate its hash code from base
                );
        }
    }
}