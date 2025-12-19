using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.Tweening
{
    public abstract class SequenceBase : TweenBase
    {
        [NotNull, ItemNotNull] private readonly IReadOnlyList<ITween> _tweens;

        protected SequenceBase(
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
            [NotNull, ItemNotNull] IEnumerable<ITween> tweens) : base(autoPlay, delayBeforeS, delayAfterS, repetitions, repetitionType, delayManagementRepetition, delayManagementRestart, onStep, onStartIteration, onStartPlay, onPlay, onEndPlay, onEndIteration, onComplete, onPause, onResume, onRestart)
        {
            ArgumentNullException.ThrowIfNull(tweens);

            List<ITween> tweensCopy = new();

            foreach (ITween tween in tweens)
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
            HashCode hashCode = new();

            hashCode.Add(base.GetHashCode());

            foreach (ITween tween in _tweens)
            {
                hashCode.Add(tween.GetHashCode());
            }

            return hashCode.ToHashCode();
        }

        private bool Equals([NotNull] SequenceBase other)
        {
            ArgumentNullException.ThrowIfNull(other);

            if (_tweens.Count != other._tweens.Count)
            {
                return false;
            }

            for (int i = 0; i < _tweens.Count; ++i)
            {
                if (!_tweens[i].Equals(other._tweens[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}