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
        public readonly int MovesAmount;
        [NotNull] public readonly DestroyPlayerPieceEvent DestroyPlayerPieceEvent;

        public LockPlayerPieceEvent(
            [NotNull] InstantiatePieceEvent instantiatePieceEvent,
            Coordinate sourceCoordinate,
            int movesAmount)
        {
            ArgumentNullException.ThrowIfNull(instantiatePieceEvent);

            InstantiatePieceEvent = instantiatePieceEvent;
            SourceCoordinate = sourceCoordinate;
            MovesAmount = movesAmount;
            DestroyPlayerPieceEvent = new DestroyPlayerPieceEvent(DestroyPieceReason.Lock);
        }
    }
}