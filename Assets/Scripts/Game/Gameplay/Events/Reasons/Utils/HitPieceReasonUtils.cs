using Infrastructure.System.Exceptions;

namespace Game.Gameplay.Events.Reasons.Utils
{
    public static class HitPieceReasonUtils
    {
        public static HitPieceReason GetFrom(MovePieceReason movePieceReason)
        {
            switch (movePieceReason)
            {
                case MovePieceReason.Gravity:
                    return HitPieceReason.Gravity;
                case MovePieceReason.Lock:
                    return HitPieceReason.Lock;
                default:
                    ArgumentOutOfRangeException.Throw(movePieceReason);
                    return HitPieceReason.Gravity;
            }
        }
    }
}