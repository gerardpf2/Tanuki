namespace Infrastructure.Tweening
{
    public enum TweenState
    {
        SetUp,

        StartIteration,
        WaitBefore,
        StartPlay,
        Play,
        EndPlay,
        WaitAfter,
        EndIteration,
        PrepareRepetition,

        Pause,
        Resume,
        Restart,
        Complete
    }
}