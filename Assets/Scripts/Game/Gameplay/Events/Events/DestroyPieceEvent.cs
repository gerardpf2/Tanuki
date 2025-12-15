using System.Collections.Generic;
using Game.Gameplay.Events.Reasons;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Events.Events
{
    public class DestroyPieceEvent : IEvent
    {
        public readonly SetGoalCurrentAmountEvent SetGoalCurrentAmountEvent;
        [ItemNotNull] public readonly IReadOnlyCollection<InstantiatePieceEvent> InstantiatePieceEventsDecompose;
        public readonly int PieceId;
        public readonly DestroyPieceReason DestroyPieceReason;

        public DestroyPieceEvent(
            SetGoalCurrentAmountEvent setGoalCurrentAmountEvent,
            [ItemNotNull] IEnumerable<InstantiatePieceEvent> instantiatePieceEventsDecompose,
            int pieceId,
            DestroyPieceReason destroyPieceReason)
        {
            SetGoalCurrentAmountEvent = setGoalCurrentAmountEvent;
            PieceId = pieceId;
            DestroyPieceReason = destroyPieceReason;

            if (instantiatePieceEventsDecompose is null)
            {
                return;
            }

            List<InstantiatePieceEvent> instantiatePieceEventsDecomposeCopy = new();

            foreach (InstantiatePieceEvent instantiatePieceEvent in instantiatePieceEventsDecompose)
            {
                ArgumentNullException.ThrowIfNull(instantiatePieceEvent);

                instantiatePieceEventsDecomposeCopy.Add(instantiatePieceEvent);
            }

            if (instantiatePieceEventsDecomposeCopy.Count <= 0)
            {
                return;
            }

            InstantiatePieceEventsDecompose = instantiatePieceEventsDecomposeCopy;
        }
    }
}