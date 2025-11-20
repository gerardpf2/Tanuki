using System;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.ModelViewViewModel;
using Infrastructure.Unity.Animator;
using Infrastructure.Unity.Utils;
using UnityEngine;
using ArgumentException = Infrastructure.System.Exceptions.ArgumentException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.Pieces.Pieces
{
    public abstract class BasePieceViewModel<T> : ViewModel, IDataSettable<IPiece>, IPieceViewEventNotifier, IAnimationEventNotifier where T : IPiece
    {
        [SerializeField] private Transform _content; // TODO: Use bindings

        protected T Piece;

        private Action _animationEndCallback;

        public void SetData(IPiece data)
        {
            ArgumentException.ThrowIfTypeIsNot<T>(data);

            Piece = (T)data;

            SyncState();
        }

        public void OnInstantiated(InstantiatePieceReason instantiatePieceReason, Action onComplete)
        {
            // TODO

            onComplete?.Invoke();
        }

        public void OnDestroyed(DestroyPieceReason destroyPieceReason, Action onComplete)
        {
            // TODO

            onComplete?.Invoke();
        }

        public void OnRotated()
        {
            SyncRotation();
        }

        public void OnAnimationEnd(string _)
        {
            /*
             *
             * Ideally animationName should be compared with the one that could have been set at SetAnimationEndCallback
             * in order to determine if the animation that has ended is the expected one
             *
             * But this is something that cannot be done, at least for pieces, because one piece can use different
             * animations for each animation trigger and another can use the exact same animation for all them. In this
             * last case it is not clear which animation name should be set in the animator state
             *
             */

            if (_animationEndCallback is null)
            {
                InvalidOperationException.Throw(); // TODO
            }

            _animationEndCallback();
            _animationEndCallback = null;
        }

        protected virtual void SyncState()
        {
            SyncRotation();
        }

        protected void SetAnimationEndCallback(Action animationEndCallback)
        {
            if (_animationEndCallback is not null)
            {
                InvalidOperationException.Throw(); // TODO
            }

            _animationEndCallback = animationEndCallback;
        }

        private void SyncRotation()
        {
            InvalidOperationException.ThrowIfNull(_content);
            InvalidOperationException.ThrowIfNull(Piece);

            _content.localPosition = _content.localPosition.WithX(0.5f * (Piece.Width - 1)).WithY(0.5f * Piece.Height);
            _content.localRotation = Quaternion.Euler(0.0f, 0.0f, -90.0f * Piece.Rotation); // Clockwise rotation
        }
    }
}