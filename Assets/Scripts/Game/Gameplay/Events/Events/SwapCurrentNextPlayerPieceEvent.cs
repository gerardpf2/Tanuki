using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Events.Events
{
    public class SwapCurrentNextPlayerPieceEvent : IEvent
    {
        [NotNull] public readonly InstantiatePieceEvent InstantiatePieceEvent;

        public SwapCurrentNextPlayerPieceEvent([NotNull] InstantiatePieceEvent instantiatePieceEvent)
        {
            ArgumentNullException.ThrowIfNull(instantiatePieceEvent);

            InstantiatePieceEvent = instantiatePieceEvent;
        }
    }
}