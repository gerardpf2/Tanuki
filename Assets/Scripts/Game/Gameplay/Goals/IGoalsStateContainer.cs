using System.Collections.Generic;
using Game.Gameplay.Board;
using JetBrains.Annotations;

namespace Game.Gameplay.Goals
{
    public interface IGoalsStateContainer
    {
        [NotNull]
        IEnumerable<PieceType> PieceTypes { get; }

        void Initialize(IEnumerable<IGoalDefinition> initialGoalDefinitions);

        void Uninitialize();

        int GetInitialAmount(PieceType pieceType);

        int GetCurrentAmount(PieceType pieceType);

        void TryRegisterDestroyed(PieceType pieceType);
    }
}