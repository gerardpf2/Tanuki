using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.Tweening
{
    public abstract class SequenceBase : TweenBase
    {
        [NotNull, ItemNotNull] private readonly List<ITween> _tweens = new();

        protected SequenceBase(
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
            [NotNull, ItemNotNull] IEnumerable<ITween> tweens) : base(autoPlay, delayBeforeS, delayAfterS, repetitions, repetitionType, delayManagementRepetition, delayManagementRestart, onStartIteration, onStartPlay, onEndPlay, onEndIteration, onPause, onResume, onRestart, onComplete)
        {
            ArgumentNullException.ThrowIfNull(tweens);

            foreach (ITween tween in tweens)
            {
                ArgumentNullException.ThrowIfNull(tween);

                _tweens.Add(tween);
            }
        }

        protected override float Play(float deltaTimeS, bool backwards)
        {
            return Play(deltaTimeS, backwards, _tweens);
        }

        protected override void OnRestart()
        {
            base.OnRestart();

            RestartTweens();
        }

        protected override void OnPrepareRepetition()
        {
            base.OnPrepareRepetition();

            RestartTweens();
        }

        protected abstract float Play(
            float deltaTimeS,
            bool backwards,
            [NotNull, ItemNotNull] IReadOnlyList<ITween> tweens
        );

        private void RestartTweens()
        {
            foreach (ITween tween in _tweens)
            {
                tween.Restart();
            }
        }
    }
}