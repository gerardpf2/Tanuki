using Game.Gameplay.Board;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Events.Events
{
    public class LockPlayerPieceEvent : IEvent
    {
        [NotNull] public readonly InstantiatePieceEvent InstantiatePieceEvent;
        public readonly Coordinate SourceCoordinate;
        public readonly int MovesAmount;

        public LockPlayerPieceEvent(
            [NotNull] InstantiatePieceEvent instantiatePieceEvent,
            Coordinate sourceCoordinate,
            int movesAmount)
        {
            ArgumentNullException.ThrowIfNull(instantiatePieceEvent);

            InstantiatePieceEvent = instantiatePieceEvent;
            SourceCoordinate = sourceCoordinate;
            MovesAmount = movesAmount;
        }
    }
}