using JetBrains.Annotations;

namespace Game.Gameplay.Board
{
    public interface IBoardController
    {
        [NotNull]
        IReadonlyBoard Initialize(string boardId);

        void ResolveInstantiateInitialAndCascade();
    }
}