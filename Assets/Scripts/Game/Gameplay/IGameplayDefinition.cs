namespace Game.Gameplay
{
    public interface IGameplayDefinition
    {
        string Id { get; }

        string Board { get; }

        string Goals { get; }
    }
}