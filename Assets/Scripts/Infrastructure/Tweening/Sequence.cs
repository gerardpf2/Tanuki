using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.Tweening
{
    public class Sequence : SequenceBase<ISequence>, ISequence
    {
        protected override ISequence This => this;

        public Sequence(
            bool autoPlay,
            float delayBeforeS,
            float delayAfterS,
            int repetitions,
            RepetitionType repetitionType,
            DelayManagement delayManagementRepetition,
            DelayManagement delayManagementRestart,
            Action<ISequence> onStep,
            Action<ISequence> onStartIteration,
            Action<ISequence> onStartPlay,
            Action<ISequence> onPlay,
            Action<ISequence> onEndPlay,
            Action<ISequence> onEndIteration,
            Action<ISequence> onComplete,
            Action<ISequence> onPause,
            Action<ISequence> onResume,
            Action<ISequence> onRestart,
            [NotNull, ItemNotNull] IEnumerable<ITweenBase> tweens) : base(autoPlay, delayBeforeS, delayAfterS, repetitions, repetitionType, delayManagementRepetition, delayManagementRestart, onStep, onStartIteration, onStartPlay, onPlay, onEndPlay, onEndIteration, onComplete, onPause, onResume, onRestart, tweens) { }

        protected override float Play(float deltaTimeS, bool backwards, IReadOnlyList<ITweenBase> tweens)
        {
            ArgumentNullException.ThrowIfNull(tweens);

            backwards ^= Backwards;

            for (int i = 0; deltaTimeS > 0.0f && i < tweens.Count; ++i)
            {
                int index = backwards ? tweens.Count - 1 - i : i;

                ITweenBase tween = tweens[index];

                ArgumentNullException.ThrowIfNull(tween);

                deltaTimeS = tween.Update(deltaTimeS, backwards);
            }

            return deltaTimeS;
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj)) // Already checks null / ReferenceEquals
            {
                return false;
            }

            return obj is Sequence;
        }

        public override int GetHashCode()
        {
            return
                HashCode.Combine(
                    base.GetHashCode(),
                    typeof(Sequence) // Not sure if it is needed, but since Sequence has no new fields, this should differentiate its hash code from base
                );
        }
    }
}