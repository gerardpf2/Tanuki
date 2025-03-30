namespace Infrastructure.Tweening.TweenBuilders
{
    public class SequenceBuilder : SequenceBaseBuilderHelper<ISequenceBuilder>, ISequenceBuilder
    {
        protected override ISequenceBuilder This => this;

        protected override ITween BuildTween()
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