namespace Game.Gameplay.Board.Pieces
{
    public interface IPieceUpdater
    {
        void Damage(int rowOffset, int columnOffset);
    }
}