namespace Infrastructure.Tweening.TweenBuilders
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