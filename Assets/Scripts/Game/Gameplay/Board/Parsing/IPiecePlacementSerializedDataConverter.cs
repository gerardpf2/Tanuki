using JetBrains.Annotations;

namespace Game.Gameplay.Board.Parsing
{
    public interface IPiecePlacementSerializedDataConverter
    {
        [NotNull]
        IPiecePlacement To(PiecePlacementSerializedData piecePlacementSerializedData);

        [NotNull]
        PiecePlacementSerializedData From(IPiecePlacement piecePlacement);
    }
}