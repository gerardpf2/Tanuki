namespace Game.Gameplay.Board.Pieces
{
    public interface ITest : IPiece
    {
        public const int Rows = 3;

        bool EyeMovementDirectionUp { get; }

        int EyeRowOffset { get; }
    }
}