namespace Infrastructure.Tweening.TweenBuilders
{
    public static class SequenceAsyncBuilderDefaults
    {
        public const bool AutoPlay = true;
        public const RepetitionType RepetitionType = Tweening.RepetitionType.Restart;
        public const DelayManagement DelayManagementRepetition = DelayManagement.BeforeAndAfter;
        public const DelayManagement DelayManagementRestart = DelayManagement.BeforeAndAfter;
    }
}