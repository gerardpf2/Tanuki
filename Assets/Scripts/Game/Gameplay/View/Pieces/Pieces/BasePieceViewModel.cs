using System;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.ModelViewViewModel;
using Infrastructure.Unity.Utils;
using UnityEngine;
using ArgumentException = Infrastructure.System.Exceptions.ArgumentException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.Pieces.Pieces
{
    public abstract class BasePieceViewModel<T> : ViewModel, IDataSettable<IPiece>, IPieceViewEventNotifier where T : IPiece
    {
        [SerializeField] private Transform _content;

        protected T Piece;

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

        protected virtual void SyncState()
        {
            SyncRotation();
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