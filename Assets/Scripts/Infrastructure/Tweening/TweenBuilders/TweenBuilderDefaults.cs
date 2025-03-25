using UnityEngine.UIElements;

namespace Infrastructure.Tweening.TweenBuilders
{
    public static class TweenBuilderDefaults
    {
        public const bool AutoPlay = true;
        public const RepetitionType RepetitionType = Tweening.RepetitionType.RestartWithDelay;
        public const EasingMode EasingMode = UnityEngine.UIElements.EasingMode.EaseOut;
    }
}