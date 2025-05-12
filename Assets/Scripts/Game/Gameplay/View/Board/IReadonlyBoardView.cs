using Game.Gameplay.Board;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Board
{
    public interface IReadonlyBoardView
    {
        [NotNull]
        IReadonlyBoard Board { get; }
    }
}