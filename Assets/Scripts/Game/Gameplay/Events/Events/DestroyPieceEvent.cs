using System.Collections.Generic;
using Game.Common.Pieces;
using Game.Gameplay.Board;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Events.Events
{
    public class DestroyPieceEvent : IEvent
    {
        public class GoalCurrentAmountUpdatedData
        {
            public readonly PieceType PieceType;
            public readonly int CurrentAmount;
            public readonly Coordinate Coordinate;

            public GoalCurrentAmountUpdatedData(PieceType pieceType, int currentAmount, Coordinate coordinate)
            {
                PieceType = pieceType;
                CurrentAmount = currentAmount;
                Coordinate = coordinate;
            }
        }

        public class DecomposePieceData
        {
            [NotNull, ItemNotNull] private readonly ICollection<PiecePlacement> _piecePlacements = new List<PiecePlacement>(); // ItemNotNull as long as all Add check for null

            [NotNull, ItemNotNull]
            public IEnumerable<PiecePlacement> PiecePlacements => _piecePlacements;

            public void Add([NotNull] IPiece piece, Coordinate sourceCoordinate)
            {
                ArgumentNullException.ThrowIfNull(piece);

                PiecePlacement piecePlacement =
                    new(
                        piece.Clone(), // Clone needed so model and view boards have different piece refs
                        sourceCoordinate
                    );

                _piecePlacements.Add(piecePlacement);
            }
        }

        public readonly int PieceId;
        public readonly DestroyPieceReason DestroyPieceReason;
        public readonly GoalCurrentAmountUpdatedData GoalData;
        public readonly DecomposePieceData DecomposeData;

        public DestroyPieceEvent(
            int pieceId,
            DestroyPieceReason destroyPieceReason,
            GoalCurrentAmountUpdatedData goalData,
            DecomposePieceData decomposeData)
        {
            PieceId = pieceId;
            DestroyPieceReason = destroyPieceReason;
            GoalData = goalData;
            DecomposeData = decomposeData;
        }
    }
}