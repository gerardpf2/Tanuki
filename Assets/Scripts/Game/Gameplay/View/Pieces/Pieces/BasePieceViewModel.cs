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
        private readonly struct AnimationCallback
        {
            public readonly string AnimationName;
            public readonly Action Callback;

            public AnimationCallback(string animationName, Action callback)
            {
                AnimationName = animationName;
                Callback = callback;
            }
        }

        [SerializeField] private Transform _content; // TODO: Use bindings

        protected T Piece;

        private AnimationCallback? _currentAnimationEndCallback;

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

        public void OnAnimationEnd(string animationName)
        {
            if (!_currentAnimationEndCallback.HasValue)
            {
                InvalidOperationException.Throw(); // TODO
            }

            AnimationCallback animationCallback = _currentAnimationEndCallback.Value;

            if (animationCallback.AnimationName != animationName)
            {
                InvalidOperationException.Throw(); // TODO
            }

            animationCallback.Callback?.Invoke();

            _currentAnimationEndCallback = null;
        }

        protected virtual void SyncState()
        {
            SyncRotation();
        }

        protected void SetAnimationEndCallback(string animationName, Action callback)
        {
            if (_currentAnimationEndCallback.HasValue)
            {
                InvalidOperationException.Throw(); // TODO
            }

            _currentAnimationEndCallback = new AnimationCallback(animationName, callback);
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