namespace Game.Gameplay.Parsing
{
    public interface IGameplayParser
    {
        string Serialize();

        void Deserialize(string value);
    }
}