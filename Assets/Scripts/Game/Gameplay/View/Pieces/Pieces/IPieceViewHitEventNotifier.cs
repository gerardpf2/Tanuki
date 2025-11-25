using Game.Common;
using Game.Gameplay.Events.Reasons;

namespace Game.Gameplay.View.Pieces.Pieces
{
    public interface IPieceViewHitEventNotifier
    {
        void OnHit(HitPieceReason hitPieceReason, Direction direction);
    }
}