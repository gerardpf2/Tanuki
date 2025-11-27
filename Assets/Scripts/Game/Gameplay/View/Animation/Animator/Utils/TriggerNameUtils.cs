using Game.Common;
using Game.Gameplay.Events.Reasons;

namespace Game.Gameplay.View.Animation.Animator.Utils
{
    public static class TriggerNameUtils
    {
        private const string Start = nameof(Start);
        private const string End = nameof(End);

        private const string Damage = nameof(Damage);
        private const string Move = nameof(Move);
        private const string Hit = nameof(Hit);

        #region Damage

        public static string GetDamageBase()
        {
            return $"{Damage}";
        }

        public static string Get(DamagePieceReason damagePieceReason)
        {
            return $"{GetDamageBase()}_{damagePieceReason}";
        }

        public static string Get(DamagePieceReason damagePieceReason, Direction direction)
        {
            return $"{Get(damagePieceReason)}_{direction}";
        }

        #endregion

        #region Move

        public static string GetStart(MovePieceReason movePieceReason)
        {
            return $"{Move}_{movePieceReason}_{Start}";
        }

        public static string GetEnd(MovePieceReason movePieceReason)
        {
            return $"{Move}_{movePieceReason}_{End}";
        }

        #endregion

        #region Hit

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

        #endregion
    }
}