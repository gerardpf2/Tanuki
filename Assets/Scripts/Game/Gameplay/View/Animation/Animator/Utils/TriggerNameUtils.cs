using Game.Common;
using Game.Gameplay.Events.Reasons;

namespace Game.Gameplay.View.Animation.Animator.Utils
{
    public static class TriggerNameUtils
    {
        private const string Start = nameof(Start);
        private const string End = nameof(End);

        private const string Move = nameof(Move);
        private const string Hit = nameof(Hit);

        public static string GetStart(MovePieceReason movePieceReason)
        {
            return $"{Move}_{movePieceReason}_{Start}";
        }

        public static string GetEnd(MovePieceReason movePieceReason)
        {
            return $"{Move}_{movePieceReason}_{End}";
        }

        public static string GetHitBase()
        {
            return $"{Hit}";
        }

        public static string Get(HitPieceReason hitPieceReason)
        {
            return $"{GetHitBase()}_{hitPieceReason}";
        }

        public static string Get(HitPieceReason hitPieceReason, Direction direction)
        {
            return $"{Get(hitPieceReason)}_{direction}";
        }
    }
}