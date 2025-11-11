using JetBrains.Annotations;

namespace Game.Gameplay.Board.Parsing
{
    public interface IBoardSerializedDataConverter
    {
        void To(BoardSerializedData boardSerializedData, IBoard board);

        [NotNull]
        BoardSerializedData From(IBoard board);
    }
}