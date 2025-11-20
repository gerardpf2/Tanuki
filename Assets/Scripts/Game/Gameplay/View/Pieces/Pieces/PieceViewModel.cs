using System;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Animation.Animator.Utils;
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
                SetAnimationEndCallback("FallStart", onComplete);

                string triggerName = TriggerNameUtils.GetStart(movePieceReason);

                _animator.SetTrigger(triggerName);
            }
            else
            {
                onComplete?.Invoke();
            }
        }

        public void OnEndMovement(MovePieceReason movePieceReason, Action onComplete)
        {
            // TODO

            if (_animator)
            {
                SetAnimationEndCallback("FallEnd", onComplete);

                string triggerName = TriggerNameUtils.GetEnd(movePieceReason);

                _animator.SetTrigger(triggerName);
            }
            else
            {
                onComplete?.Invoke();
            }
        }
    }
}