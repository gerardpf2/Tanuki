using System;
using Game.Common;
using Game.Common.Utils;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Animation.Animator.Utils;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.Pieces.Pieces
{
    public class PieceViewModel : PieceViewModel<IPiece> { }

    public abstract class PieceViewModel<T> : BasePieceViewModel<T>, IBoardPieceViewEventNotifier where T : IPiece
    {
        public void OnDamaged(DamagePieceReason damagePieceReason, Action onComplete)
        {
            // TODO

            onComplete?.Invoke();
        }

        public void OnStartMovement(MovePieceReason movePieceReason, Action onComplete)
        {
            PrepareAnimation(TriggerNameUtils.GetStart(movePieceReason), onComplete);
        }

        public void OnEndMovement(MovePieceReason movePieceReason, Action onComplete)
        {
            PrepareAnimation(TriggerNameUtils.GetEnd(movePieceReason), onComplete);
        }

        public void OnHit(HitPieceReason hitPieceReason, Direction direction)
        {
            IPiece piece = Piece;

            InvalidOperationException.ThrowIfNull(piece);

            direction = direction.GetRotated(piece.Rotation);

            // TODO
        }
    }
}