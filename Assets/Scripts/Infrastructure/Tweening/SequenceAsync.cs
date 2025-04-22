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
            Action onStartIteration,
            Action onStartPlay,
            Action onEndPlay,
            Action onEndIteration,
            Action onPause,
            Action onResume,
            Action onRestart,
            Action onComplete,
            [NotNull] [ItemNotNull] IEnumerable<ITween> tweens) : base(autoPlay, delayBeforeS, delayAfterS, repetitions, repetitionType, delayManagementRepetition, delayManagementRestart, onStartIteration, onStartPlay, onEndPlay, onEndIteration, onPause, onResume, onRestart, onComplete, tweens) { }

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
    }
}