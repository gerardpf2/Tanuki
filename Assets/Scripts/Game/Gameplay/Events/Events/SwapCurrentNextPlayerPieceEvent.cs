using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Events.Events
{
    public class SwapCurrentNextPlayerPieceEvent : IEvent
    {
        [NotNull] public readonly InstantiatePlayerPieceEvent InstantiatePlayerPieceEvent;

        public SwapCurrentNextPlayerPieceEvent([NotNull] InstantiatePlayerPieceEvent instantiatePlayerPieceEvent)
        {
            ArgumentNullException.ThrowIfNull(instantiatePlayerPieceEvent);

            InstantiatePlayerPieceEvent = instantiatePlayerPieceEvent;
        }
    }
}