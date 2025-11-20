using Game.Gameplay.Events.Reasons;

namespace Game.Gameplay.View.Animation.Animator.Utils
{
    public static class TriggerNameUtils
    {
        private const string Move = nameof(Move);
        private const string Start = nameof(Start);
        private const string End = nameof(End);

        public static string GetStart(MovePieceReason movePieceReason)
        {
            return $"{Move}_{movePieceReason}_{Start}";
        }

        public static string GetEnd(MovePieceReason movePieceReason)
        {
            return $"{Move}_{movePieceReason}_{End}";
        }
    }
}