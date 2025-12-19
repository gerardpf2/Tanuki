using System;

namespace Infrastructure.Tweening.Builders
{
    public class SequenceAsyncBuilder : SequenceBaseBuilderHelper<ISequenceAsyncBuilder>, ISequenceAsyncBuilder
    {
        protected override ISequenceAsyncBuilder This => this;

        protected override ITween BuildTween()
        {
            return
                new SequenceAsync(
                    AutoPlay,
                    DelayBeforeS,
                    DelayAfterS,
                    Repetitions,
                    RepetitionType,
                    DelayManagementRepetition,
                    DelayManagementRestart,
                    OnStep,
                    OnStartIteration,
                    OnStartPlay,
                    OnPlay,
                    OnEndPlay,
                    OnEndIteration,
                    OnComplete,
                    OnPause,
                    OnResume,
                    OnRestart,
                    Tweens
                );
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is SequenceAsyncBuilder;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(typeof(SequenceAsyncBuilder));
        }
    }
}