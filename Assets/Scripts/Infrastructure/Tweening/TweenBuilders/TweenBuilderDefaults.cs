using UnityEngine.UIElements;

namespace Infrastructure.Tweening.TweenBuilders
{
    public static class TweenBuilderDefaults
    {
        public const bool AutoPlay = true;
        public const RepetitionType RepetitionType = Tweening.RepetitionType.Restart;
        public const DelayManagement DelayManagementRepetition = DelayManagement.BeforeAndAfter;
        public const DelayManagement DelayManagementRestart = DelayManagement.BeforeAndAfter;
        public const EasingMode EasingMode = UnityEngine.UIElements.EasingMode.EaseOut;
    }
}