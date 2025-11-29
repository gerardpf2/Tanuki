using System.Collections.Generic;
using Game.Gameplay.Events.Reasons;

namespace Game.Gameplay.Events.Events
{
    public class DestroyPieceEvent : IEvent
    {
        public readonly UpdateGoalEvent UpdateGoalEvent;
        public readonly IReadOnlyCollection<InstantiatePieceEvent> InstantiatePieceEventsDecompose;
        public readonly int PieceId;
        public readonly DestroyPieceReason DestroyPieceReason;

        public DestroyPieceEvent(
            UpdateGoalEvent updateGoalEvent,
            IReadOnlyCollection<InstantiatePieceEvent> instantiatePieceEventsDecompose,
            int pieceId,
            DestroyPieceReason destroyPieceReason)
        {
            UpdateGoalEvent = updateGoalEvent;
            InstantiatePieceEventsDecompose = instantiatePieceEventsDecompose;
            PieceId = pieceId;
            DestroyPieceReason = destroyPieceReason;
        }
    }
}