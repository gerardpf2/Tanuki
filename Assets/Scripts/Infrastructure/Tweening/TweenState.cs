namespace Infrastructure.Tweening
{
    public enum TweenState
    {
        SetUp,

        StartIteration,
        WaitBefore,
        Play,
        WaitAfter,
        EndIteration,

        Paused,
        Completed
    }
}