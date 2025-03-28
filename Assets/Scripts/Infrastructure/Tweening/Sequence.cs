using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.Tweening
{
    public class Sequence : SequenceBase
    {
        public Sequence(
            bool autoPlay,
            float delayBeforeS,
            float delayAfterS,
            int repetitions,
            RepetitionType repetitionType,
            DelayManagement delayManagementRepetition,
            DelayManagement delayManagementRestart,
            Action onEndIteration,
            Action onCompleted,
            [NotNull] [ItemNotNull] IEnumerable<ITween> tweens) : base(autoPlay, delayBeforeS, delayAfterS, repetitions, repetitionType, delayManagementRepetition, delayManagementRestart, onEndIteration, onCompleted, tweens) { }

        protected override float Refresh(float deltaTimeS, bool backwards, IReadOnlyList<ITween> tweens)
        {
            ArgumentNullException.ThrowIfNull(tweens);

            backwards ^= Backwards;

            for (int i = 0; deltaTimeS > 0.0f && i < tweens.Count; ++i)
            {
                int index = backwards ? tweens.Count - 1 - i : i;

                ITween tween = tweens[index];

                ArgumentNullException.ThrowIfNull(tween);

                deltaTimeS = tween.Update(deltaTimeS, backwards);
            }

            return deltaTimeS;
        }
    }
}