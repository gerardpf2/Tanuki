using System;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.View.Animation.Animator.Utils;
using Infrastructure.System;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.Pieces.Pieces
{
    public class PlayerPieceViewModel : PieceViewModel, IPieceViewMoveEventNotifier
    {
        public void OnStartMovement(MovePieceReason movePieceReason, Action onComplete)
        {
            InvalidOperationException.ThrowIfNot(movePieceReason, ComparisonOperator.EqualTo, MovePieceReason.Lock);

            PrepareMainAnimation(TriggerNameUtils.GetStart(movePieceReason), onComplete);
        }

        public void OnEndMovement(MovePieceReason movePieceReason, Action onComplete)
        {
            InvalidOperationException.ThrowIfNot(movePieceReason, ComparisonOperator.EqualTo, MovePieceReason.Lock);

            PrepareMainAnimation(TriggerNameUtils.GetEnd(movePieceReason), onComplete);
        }
    }
}