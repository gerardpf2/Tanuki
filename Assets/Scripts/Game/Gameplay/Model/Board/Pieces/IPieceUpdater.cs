namespace Game.Gameplay.Model.Board.Pieces
{
    public interface IPieceUpdater
    {
        void Damage(int rowOffset, int columnOffset);
    }
}