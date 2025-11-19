using System;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using UnityEngine;

namespace Game.Gameplay.View.Pieces.Pieces
{
    public class PieceViewModel : PieceViewModel<IPiece> { }

    public abstract class PieceViewModel<T> : BasePieceViewModel<T>, IBoardPieceViewEventNotifier where T : IPiece
    {
        [SerializeField] private Animator _animator; // TODO: Use bindings

        public void OnDamaged(DamagePieceReason damagePieceReason, Action onComplete)
        {
            // TODO

            onComplete?.Invoke();
        }

        public void OnStartMovement(MovePieceReason movePieceReason, Action onComplete)
        {
            // TODO

            if (_animator)
            {
                _animator.SetBool("Fall", true);
            }

            onComplete?.Invoke();
        }

        public void OnEndMovement(MovePieceReason movePieceReason, Action onComplete)
        {
            // TODO

            if (_animator)
            {
                _animator.SetBool("Fall", false);
            }

            onComplete?.Invoke();
        }
    }
}