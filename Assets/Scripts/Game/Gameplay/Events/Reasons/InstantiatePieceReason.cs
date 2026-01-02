namespace Game.Gameplay.Events.Reasons
{
    public enum InstantiatePieceReason
    {
        // Board
        Initial,
        Lock,
        Decompose,

        // Player
        Regular,
        SwapCurrentNext
    }
}