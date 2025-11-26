using System;
using Game.Common;
using Game.Common.Utils;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Animation.Animator.Utils;
using Game.Gameplay.View.Pieces.EventNotifiers;
using Infrastructure.System;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.Pieces.Pieces.BoardPieces
{
    public class BoardPieceViewModel : BoardPieceViewModel<IPiece> { }

    public abstract class BoardPieceViewModel<TPiece> : PieceViewModel<TPiece>, IPieceViewDamageEventNotifier, IPieceViewMoveEventNotifier, IPieceViewHitEventNotifier where TPiece : IPiece
    {
        public void OnDamaged(DamagePieceReason damagePieceReason, Direction direction, Action onComplete)
        {
            direction = GetRotated(direction);

            PrepareMainAnimation(
                onComplete,
                TriggerNameUtils.Get(damagePieceReason, direction),
                TriggerNameUtils.Get(damagePieceReason),
                TriggerNameUtils.GetDamageBase()
            );
        }

        public void OnMovementStarted(MovePieceReason movePieceReason, Action onComplete)
        {
            InvalidOperationException.ThrowIfNot(movePieceReason, ComparisonOperator.UnequalTo, MovePieceReason.Lock);

            PrepareMainAnimation(TriggerNameUtils.GetStart(movePieceReason), onComplete);
        }

        public void OnMovementEnded(MovePieceReason movePieceReason, Action onComplete)
        {
            InvalidOperationException.ThrowIfNot(movePieceReason, ComparisonOperator.UnequalTo, MovePieceReason.Lock);

            PrepareMainAnimation(TriggerNameUtils.GetEnd(movePieceReason), onComplete);
        }

        public void OnHit(HitPieceReason hitPieceReason, Direction direction)
        {
            direction = GetRotated(direction);

            PrepareSecondaryAnimation(
                TriggerNameUtils.Get(hitPieceReason, direction),
                TriggerNameUtils.Get(hitPieceReason),
                TriggerNameUtils.GetHitBase()
            );
        }

        private Direction GetRotated(Direction direction)
        {
            IPiece piece = Piece;

            InvalidOperationException.ThrowIfNull(piece);

            return direction.GetRotated(piece.Rotation);
        }
    }
}