using System.Collections.Generic;
using Game.Common.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.Goals
{
    public interface IGoals
    {
        [NotNull]
        IEnumerable<PieceType> PieceTypes { get; }

        [NotNull]
        IGoal Get(PieceType pieceType);

        [ContractAnnotation("=> true, goal:notnull; => false, goal:null")]
        bool TryGet(PieceType pieceType, out IGoal goal);

        [NotNull]
        IGoals Clone();
    }
}