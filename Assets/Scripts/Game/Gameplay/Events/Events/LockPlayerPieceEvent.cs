using Game.Gameplay.Board;

namespace Game.Gameplay.Events.Events
{
    public class LockPlayerPieceEvent : IEvent
    {
        public readonly InstantiatePieceEvent InstantiatePieceEvent;
        public readonly Coordinate SourceCoordinate;
        public readonly int MovesAmount;

        public LockPlayerPieceEvent(
            InstantiatePieceEvent instantiatePieceEvent,
            Coordinate sourceCoordinate,
            int movesAmount)
        {
            InstantiatePieceEvent = instantiatePieceEvent;
            SourceCoordinate = sourceCoordinate;
            MovesAmount = movesAmount;
        }
    }
}