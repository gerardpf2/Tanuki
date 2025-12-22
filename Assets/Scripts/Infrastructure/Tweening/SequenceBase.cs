using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.Tweening
{
    // TODO: Test properties
    public abstract class SequenceBase<TTween> : TweenBase<TTween>, ISequenceBase
    {
        [NotNull, ItemNotNull] private readonly IReadOnlyList<ITweenBase> _tweens;

        public IEnumerable<ITweenBase> Tweens => _tweens;

        protected SequenceBase(
            bool autoPlay,
            float delayBeforeS,
            float delayAfterS,
            int repetitions,
            RepetitionType repetitionType,
            DelayManagement delayManagementRepetition,
            DelayManagement delayManagementRestart,
            Action<TTween> onStep,
            Action<TTween> onStartIteration,
            Action<TTween> onStartPlay,
            Action<TTween> onPlay,
            Action<TTween> onEndPlay,
            Action<TTween> onEndIteration,
            Action<TTween> onComplete,
            Action<TTween> onPause,
            Action<TTween> onResume,
            Action<TTween> onRestart,
            [NotNull, ItemNotNull] IEnumerable<ITweenBase> tweens) : base(autoPlay, delayBeforeS, delayAfterS, repetitions, repetitionType, delayManagementRepetition, delayManagementRestart, onStep, onStartIteration, onStartPlay, onPlay, onEndPlay, onEndIteration, onComplete, onPause, onResume, onRestart)
        {
            ArgumentNullException.ThrowIfNull(tweens);

            List<ITweenBase> tweensCopy = new();

            foreach (ITweenBase tween in tweens)
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
            [NotNull, ItemNotNull] IReadOnlyList<ITweenBase> tweens
        );

        private void RestartTweens()
        {
            foreach (ITweenBase tween in _tweens)
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

            if (obj is not SequenceBase<TTween> other)
            {
                return false;
            }

            return Equals(other);
        }

        public override int GetHashCode()
        {
            HashCode hashCode = new();

            hashCode.Add(base.GetHashCode());

            foreach (ITweenBase tween in _tweens)
            {
                hashCode.Add(tween.GetHashCode());
            }

            return hashCode.ToHashCode();
        }

        private bool Equals([NotNull] SequenceBase<TTween> other)
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