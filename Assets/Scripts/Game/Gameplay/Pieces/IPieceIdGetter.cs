namespace Game.Gameplay.Pieces
{
    public interface IPieceIdGetter
    {
        void Initialize();

        void Uninitialize();

        int GetNew();
    }
}