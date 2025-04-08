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
    }
}