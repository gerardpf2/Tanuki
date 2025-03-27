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
            bool autoPlay,
            float delayBeforeS,
            float delayAfterS,
            int repetitions,
            RepetitionType repetitionType,
            DelayManagement delayManagementRepetition,
            DelayManagement delayManagementRestart,
            Action onEndIteration,
            Action onCompleted,
            [NotNull, ItemNotNull] IEnumerable<ITween> tweens) : base(autoPlay, delayBeforeS, delayAfterS, repetitions, repetitionType, delayManagementRepetition, delayManagementRestart, onEndIteration, onCompleted)
        {
            ArgumentNullException.ThrowIfNull(tweens);

            foreach (ITween tween in tweens)
            {
                ArgumentNullException.ThrowIfNull(tween);

                _tweens.Add(tween);
            }
        }

        public override void Restart()
        {
            base.Restart();

            RestartTweens();
        }

        protected override float Refresh(float deltaTimeS, bool backwards)
        {
            backwards ^= Backwards;

            while (deltaTimeS > 0.0f)
            {
                ITween tween = FirstNotCompleted(backwards);

                if (tween is null)
                {
                    break;
                }

                deltaTimeS = tween.Update(deltaTimeS, backwards);
            }

            return deltaTimeS;
        }

        protected override void PrepareRepetition()
        {
            base.PrepareRepetition();

            RestartTweens();
        }

        private void RestartTweens()
        {
            foreach (ITween tween in _tweens)
            {
                tween.Restart();
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