using Game.Gameplay.Board;
using Game.Gameplay.Events.Reasons;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Events.Events
{
    public class LockPlayerPieceEvent : IEvent
    {
        [NotNull] public readonly InstantiatePieceEvent InstantiatePieceEvent;
        public readonly Coordinate SourceCoordinate;
        [NotNull] public readonly DestroyPlayerPieceEvent DestroyPlayerPieceEvent;
        [NotNull] public readonly SetMovesAmountEvent SetMovesAmountEvent;

        public LockPlayerPieceEvent(
            [NotNull] InstantiatePieceEvent instantiatePieceEvent,
            Coordinate sourceCoordinate,
            int movesAmount)
        {
            ArgumentNullException.ThrowIfNull(instantiatePieceEvent);

            InstantiatePieceEvent = instantiatePieceEvent;
            SourceCoordinate = sourceCoordinate;
            DestroyPlayerPieceEvent = new DestroyPlayerPieceEvent(DestroyPieceReason.Lock);
            SetMovesAmountEvent = new SetMovesAmountEvent(movesAmount);
        }
    }
}