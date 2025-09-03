using System;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing.Events.Reasons;
using Infrastructure.ModelViewViewModel;
using ArgumentException = Infrastructure.System.Exceptions.ArgumentException;

namespace Game.Gameplay.View.Board.Pieces
{
    public abstract class PieceViewModel<T> : ViewModel, IDataSettable<IPiece>, IPieceViewEventNotifier where T : IPiece
    {
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

        public void OnDestroyed(Action onComplete)
        {
            // TODO

            onComplete?.Invoke();
        }

        protected virtual void SyncState() { }
    }
}