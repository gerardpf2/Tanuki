using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Goals;
using JetBrains.Annotations;

namespace Game.Gameplay.Parsing
{
    public interface IGameplaySerializedDataConverter
    {
        void To(
            GameplaySerializedData gameplaySerializedData,
            [NotNull] out IBoard board,
            [NotNull] out IEnumerable<PiecePlacement> piecePlacements,
            [NotNull] out IGoals goals
        );

        [NotNull]
        GameplaySerializedData From(IBoard board, IGoals goals);
    }
}