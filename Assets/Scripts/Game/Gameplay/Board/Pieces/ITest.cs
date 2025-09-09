namespace Game.Gameplay.Board.Pieces
{
    public interface ITest : IPiece
    {
        bool EyeMovementDirectionUp { get; }

        int EyeRowOffset { get; }
    }
}