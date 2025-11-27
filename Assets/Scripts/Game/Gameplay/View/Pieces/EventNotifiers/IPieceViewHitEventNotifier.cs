using Game.Common;
using Game.Gameplay.Events.Reasons;

namespace Game.Gameplay.View.Pieces.EventNotifiers
{
    public interface IPieceViewHitEventNotifier
    {
        void OnHit(HitPieceReason hitPieceReason, Direction direction);
    }
}