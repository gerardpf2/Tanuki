using JetBrains.Annotations;

namespace Game.Gameplay.Moves.Parsing
{
    public interface IMovesSerializedDataConverter
    {
        void To(MovesSerializedData movesSerializedData, IMoves moves);

        [NotNull]
        MovesSerializedData From(IMoves moves);
    }
}