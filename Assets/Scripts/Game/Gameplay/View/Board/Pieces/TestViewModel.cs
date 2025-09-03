using Game.Gameplay.Board.Pieces;

namespace Game.Gameplay.View.Board.Pieces
{
    public class TestViewModel : PieceViewModel<ITest>
    {
        protected override void SyncState()
        {
            base.SyncState();

            // TODO: Eye row offset, etc
        }
    }
}