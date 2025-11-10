using JetBrains.Annotations;

namespace Game.Gameplay.Board.Parsing
{
    public interface IBoardSerializedDataConverter
    {
        [NotNull]
        IBoard To(BoardSerializedData boardSerializedData);

        [NotNull]
        BoardSerializedData From(IBoard board);
    }
}