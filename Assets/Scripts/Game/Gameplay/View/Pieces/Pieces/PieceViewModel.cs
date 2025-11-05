using System;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;

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
            // TODO

            onComplete?.Invoke();
        }

        public void OnEndMovement(MovePieceReason movePieceReason, Action onComplete)
        {
            // TODO

            onComplete?.Invoke();
        }
    }
}