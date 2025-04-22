namespace Infrastructure.Tweening.Builders
{
    public class SequenceAsyncBuilder : SequenceBaseBuilderHelper<ISequenceAsyncBuilder>, ISequenceAsyncBuilder
    {
        protected override ISequenceAsyncBuilder This => this;

        public override ITween Build()
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