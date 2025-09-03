using System;
using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.EventEnqueueing.Events.Reasons;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.EventResolution.EventResolvers
{
    public class InstantiatePlayerPieceEventResolver : IEventResolver<InstantiatePlayerPieceEvent>
    {
        [NotNull] private readonly IActionFactory _actionFactory;

        public InstantiatePlayerPieceEventResolver([NotNull] IActionFactory actionFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);

            _actionFactory = actionFactory;
        }

        public void Resolve([NotNull] InstantiatePlayerPieceEvent evt, Action onComplete)
        {
            ArgumentNullException.ThrowIfNull(evt);

            _actionFactory
                .GetInstantiatePlayerPieceAction(evt.Piece, InstantiatePieceReason.Player)
                .Resolve(onComplete);
        }
    }
}