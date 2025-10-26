namespace Game.Gameplay.Pieces.Pieces
{
    public interface ITest : IPiece
    {
        bool EyeMovementDirectionUp { get; }

        int EyeRowOffset { get; }

        void MoveEye();
    }
}