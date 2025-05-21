using JetBrains.Annotations;

namespace Game.Gameplay.Board.Parsing
{
    public interface IPiecePlacementSerializedDataConverter
    {
        [NotNull]
        PiecePlacement To(PiecePlacementSerializedData piecePlacementSerializedData);

        [NotNull]
        PiecePlacementSerializedData From(PiecePlacement piecePlacement);
    }
}