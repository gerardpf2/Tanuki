using System;

namespace Infrastructure.Tweening.Builders
{
    public class SequenceBuilder : SequenceBaseBuilderHelper<ISequenceBuilder>, ISequenceBuilder
    {
        protected override ISequenceBuilder This => this;

        public override ITween Build()
        {
            return
                new Sequence(
                    AutoPlay,
                    DelayBeforeS,
                    DelayAfterS,
                    Repetitions,
                    RepetitionType,
                    DelayManagementRepetition,
                    DelayManagementRestart,
                    OnStartIteration,
                    OnStartPlay,
                    OnEndPlay,
                    OnEndIteration,
                    OnPause,
                    OnResume,
                    OnRestart,
                    OnComplete,
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

            return obj is SequenceBuilder;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(typeof(SequenceBuilder));
        }
    }
}