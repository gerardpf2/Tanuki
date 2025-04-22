using System;
using System.Collections.Generic;
using Infrastructure.System;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;

namespace Infrastructure.Tweening
{
    public abstract class SequenceBase : TweenBase
    {
        [NotNull, ItemNotNull] private readonly List<ITween> _tweens = new();

        protected SequenceBase(
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
            [NotNull, ItemNotNull] IEnumerable<ITween> tweens) : base(autoPlay, delayBeforeS, delayAfterS, repetitions, repetitionType, delayManagementRepetition, delayManagementRestart, onStartIteration, onStartPlay, onEndPlay, onEndIteration, onPause, onResume, onRestart, onComplete)
        {
            ArgumentNullException.ThrowIfNull(tweens);

            foreach (ITween tween in tweens)
            {
                ArgumentNullException.ThrowIfNull(tween);

                _tweens.Add(tween);
            }
        }

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f), Is(ComparisonOperator.LessThanOrEqualTo, "deltaTimeS")]
        protected override float Play([Is(ComparisonOperator.GreaterThan, 0.0f)] float deltaTimeS, bool backwards)
        {
            ArgumentOutOfRangeException.ThrowIfNot(deltaTimeS, ComparisonOperator.GreaterThan, 0.0f);

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

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f), Is(ComparisonOperator.LessThanOrEqualTo, "deltaTimeS")]
        protected abstract float Play(
            [Is(ComparisonOperator.GreaterThan, 0.0f)] float deltaTimeS,
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