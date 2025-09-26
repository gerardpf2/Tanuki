using System.Collections.Generic;
using Game.Gameplay.Board;
using JetBrains.Annotations;

namespace Game.Gameplay.Goals
{
    public interface IGoals
    {
        [NotNull, ItemNotNull]
        IEnumerable<IGoal> Targets { get; }

        [ContractAnnotation("=> true, goal:notnull; => false, goal:null")]
        bool TryGet(PieceType pieceType, out IGoal goal);
    }
}