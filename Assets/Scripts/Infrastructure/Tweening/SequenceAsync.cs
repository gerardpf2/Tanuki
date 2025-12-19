using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.Tweening
{
    public class SequenceAsync : SequenceBase
    {
        public SequenceAsync(
            bool autoPlay,
            float delayBeforeS,
            float delayAfterS,
            int repetitions,
            RepetitionType repetitionType,
            DelayManagement delayManagementRepetition,
            DelayManagement delayManagementRestart,
            Action onStep,
            Action onStartIteration,
            Action onStartPlay,
            Action onPlay,
            Action onEndPlay,
            Action onEndIteration,
            Action onComplete,
            Action onPause,
            Action onResume,
            Action onRestart,
            [NotNull, ItemNotNull] IEnumerable<ITween> tweens) : base(autoPlay, delayBeforeS, delayAfterS, repetitions, repetitionType, delayManagementRepetition, delayManagementRestart, onStep, onStartIteration, onStartPlay, onPlay, onEndPlay, onEndIteration, onComplete, onPause, onResume, onRestart, tweens) { }

        protected override float Play(float deltaTimeS, bool backwards, IReadOnlyList<ITween> tweens)
        {
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