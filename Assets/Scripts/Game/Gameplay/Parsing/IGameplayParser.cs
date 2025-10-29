using System.Collections.Generic;
using Game.Gameplay.Bag;
using Game.Gameplay.Board;
using Game.Gameplay.Goals;
using Game.Gameplay.Moves;
using Game.Gameplay.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.Parsing
{
    public interface IGameplayParser
    {
        string Serialize(IBoard board, IGoals goals, IMoves moves, IBag bag);

        void Deserialize(
            string value,
            [NotNull] out IBoard board,
            [NotNull] out IEnumerable<PiecePlacement> piecePlacements,
            [NotNull] out IGoals goals,
            [NotNull] out IMoves moves,
            [NotNull] out IBag bag
        );
    }
}