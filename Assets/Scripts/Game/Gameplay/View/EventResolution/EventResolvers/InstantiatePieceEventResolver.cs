using System;
using Game.Gameplay.EventEnqueueing.Events;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.EventResolution.EventResolvers
{
    public class InstantiatePieceEventResolver : IEventResolver<InstantiatePieceEvent>
    {
        [NotNull] private readonly IActionFactory _actionFactory;

        public InstantiatePieceEventResolver([NotNull] IActionFactory actionFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);

            _actionFactory = actionFactory;
        }

        public void Resolve([NotNull] InstantiatePieceEvent evt, Action onComplete)
        {
            ArgumentNullException.ThrowIfNull(evt);

            _actionFactory
                .GetInstantiatePieceAction(evt.Piece, evt.InstantiatePieceReason, evt.SourceCoordinate)
                .Resolve(onComplete);
        }
    }
}