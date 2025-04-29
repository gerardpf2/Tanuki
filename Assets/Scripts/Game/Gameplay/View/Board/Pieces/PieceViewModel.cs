using System;
using Game.Gameplay.Board.Pieces;
using Infrastructure.ModelViewViewModel;
using JetBrains.Annotations;
using ArgumentException = Infrastructure.System.Exceptions.ArgumentException;

namespace Game.Gameplay.View.Board.Pieces
{
    public abstract class PieceViewModel<T> : ViewModel, IDataSettable<IPiece>, IPieceViewEventNotifier where T : IPiece
    {
        protected T Piece;

        public void SetData([NotNull] IPiece data)
        {
            ArgumentException.ThrowIfTypeIsNot<T>(data);

            Piece = (T)data;

            SyncState();
        }

        public void OnInstantiated(Action onComplete)
        {
            // TODO: Trigger ¿?

            onComplete?.Invoke();
        }

        protected abstract void SyncState();
    }
}