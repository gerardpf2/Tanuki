using JetBrains.Annotations;

namespace Game.Gameplay.Moves.Parsing
{
    public interface IMovesParser
    {
        string Serialize(IMoves moves);

        [NotNull]
        IMoves Deserialize(string value);
    }
}