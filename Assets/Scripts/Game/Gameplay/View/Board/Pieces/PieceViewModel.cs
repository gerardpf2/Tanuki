using Game.Gameplay.Model.Board.Pieces;
using Infrastructure.ModelViewViewModel;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Board.Pieces
{
    public abstract class PieceViewModel<T> : ViewModel, IDataSettable<IPiece>, IPieceViewEventNotifier where T : IPiece
    {
        protected T Piece;

        public void SetData([NotNull] IPiece data)
        {
            // TODO: Check data is T, otherwise throw ArgumentException

            Piece = (T)data;

            SyncState();
        }

        public void OnInstantiated()
        {
            // TODO: Trigger ¿?
        }

        protected abstract void SyncState();
    }
}