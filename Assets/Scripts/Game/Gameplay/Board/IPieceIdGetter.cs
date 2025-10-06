namespace Game.Gameplay.Board
{
    public interface IPieceIdGetter
    {
        void Initialize();

        void Uninitialize();

        int GetNew();
    }
}