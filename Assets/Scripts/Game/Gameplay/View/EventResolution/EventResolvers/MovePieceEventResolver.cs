using System;
using Game.Gameplay.EventEnqueueing.Events;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.EventResolution.EventResolvers
{
    public class MovePieceEventResolver : IEventResolver<MovePieceEvent>
    {
        [NotNull] private readonly IActionFactory _actionFactory;

        public MovePieceEventResolver([NotNull] IActionFactory actionFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);

            _actionFactory = actionFactory;
        }

        public void Resolve([NotNull] MovePieceEvent evt, Action onComplete)
        {
            ArgumentNullException.ThrowIfNull(evt);

            _actionFactory
                .GetMovePieceAction(evt.Piece, evt.NewSourceCoordinate, evt.MovePieceReason)
                .Resolve(onComplete);
        }
    }
}