namespace Game.Gameplay.Board
{
    public interface IBoardDefinition
    {
        string Id { get; }

        string SerializedData { get; }
    }
}