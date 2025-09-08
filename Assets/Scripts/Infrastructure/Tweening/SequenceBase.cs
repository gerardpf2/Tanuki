using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.Tweening
{
    public abstract class SequenceBase : TweenBase
    {
        [NotNull, ItemNotNull] private readonly IReadOnlyList<ITween> _tweens;
        private readonly IEnumerable<ITween> _ctorTweens; // TODO: Remove this and check _tweens instead in both Equals and GetHashCode

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

            _ctorTweens = tweens;

            List<ITween> tweensCopy = new();

            foreach (ITween tween in _ctorTweens)
            {
                ArgumentNullException.ThrowIfNull(tween);

                tweensCopy.Add(tween);
            }

            _tweens = tweensCopy;
        }

        protected override float Play(float deltaTimeS, bool backwards)
        {
            return Play(deltaTimeS, backwards, _tweens);
        }

        protected override void OnPrepareRepetition()
        {
            base.OnPrepareRepetition();

            RestartTweens();
        }

        protected override void OnRestart()
        {
            base.OnRestart();

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

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj)) // Already checks null / ReferenceEquals
            {
                return false;
            }

            if (obj is not SequenceBase other)
            {
                return false;
            }

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), _ctorTweens);
        }

        private bool Equals([NotNull] SequenceBase other)
        {
            ArgumentNullException.ThrowIfNull(other);

            return Equals(_ctorTweens, other._ctorTweens);
        }
    }
}