using Game.Gameplay.Pieces.Pieces;

namespace Game.Gameplay.View.Pieces.Pieces.BoardPieces
{
    public class TestViewModel : BoardPieceViewModel<ITest>
    {
        protected override void SyncState()
        {
            base.SyncState();

            // TODO: Eye row offset, etc
        }
    }
}