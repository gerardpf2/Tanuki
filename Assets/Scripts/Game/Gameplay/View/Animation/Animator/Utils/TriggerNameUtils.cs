using Game.Common;
using Game.Gameplay.Events.Reasons;

namespace Game.Gameplay.View.Animation.Animator.Utils
{
    public static class TriggerNameUtils
    {
        private const string Start = nameof(Start);
        private const string End = nameof(End);

        private const string Destroy = nameof(Destroy);
        private const string Damage = nameof(Damage);
        private const string Move = nameof(Move);
        private const string Hit = nameof(Hit);

        #region Destroy

        public static string GetDestroy()
        {
            return $"{Destroy}";
        }

        public static string GetDestroy(DestroyPieceReason destroyPieceReason)
        {
            return $"{GetDestroy()}_{destroyPieceReason}";
        }

        #endregion

        #region Damage

        public static string GetDamage()
        {
            return $"{Damage}";
        }

        public static string GetDamage(DamagePieceReason damagePieceReason)
        {
            return $"{GetDamage()}_{damagePieceReason}";
        }

        public static string GetDamage(DamagePieceReason damagePieceReason, Direction direction)
        {
            return $"{GetDamage(damagePieceReason)}_{direction}";
        }

        #endregion

        #region Move

        public static string GetMove()
        {
            return $"{Move}";
        }

        public static string GetMove(MovePieceReason movePieceReason)
        {
            return $"{GetMove()}_{movePieceReason}";
        }

        public static string GetMoveStart(MovePieceReason movePieceReason)
        {
            return $"{GetMove(movePieceReason)}_{Start}";
        }

        public static string GetMoveEnd()
        {
            return $"{GetMove()}_{End}";
        }

        #endregion

        #region Hit

        public static string GetHit()
        {
            return $"{Hit}";
        }

        public static string GetHit(HitPieceReason hitPieceReason)
        {
            return $"{GetHit()}_{hitPieceReason}";
        }

        public static string GetHit(HitPieceReason hitPieceReason, Direction direction)
        {
            return $"{GetHit(hitPieceReason)}_{direction}";
        }

        #endregion
    }
}