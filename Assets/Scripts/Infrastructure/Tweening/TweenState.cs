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

        Pause,
        Resume,
        Restart,
        Complete
    }
}