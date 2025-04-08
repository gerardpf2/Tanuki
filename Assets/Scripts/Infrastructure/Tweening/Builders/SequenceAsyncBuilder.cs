namespace Infrastructure.Tweening.Builders
{
    public class SequenceAsyncBuilder : SequenceBaseBuilderHelper<ISequenceAsyncBuilder>, ISequenceAsyncBuilder
    {
        protected override ISequenceAsyncBuilder This => this;

        // TODO: Test
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