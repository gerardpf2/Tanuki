using JetBrains.Annotations;

namespace Game.Gameplay.Moves.Parsing
{
    public interface IMovesSerializedDataConverter
    {
        [NotNull]
        IMoves To(MovesSerializedData movesSerializedData);

        [NotNull]
        MovesSerializedData From(IMoves moves);
    }
}