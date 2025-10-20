using System.Collections.Generic;
using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.View.EventResolution.EventResolvers.Actions;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolution.EventResolvers
{
    public class MovePiecesByGravityEventResolver : EventResolver<MovePiecesByGravityEvent>
    {
        [NotNull] private readonly IActionFactory _actionFactory;

        public MovePiecesByGravityEventResolver([NotNull] IActionFactory actionFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);

            _actionFactory = actionFactory;
        }

        protected override IEnumerable<IAction> GetActions([NotNull] MovePiecesByGravityEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            // TODO

            yield break;
        }
    }
}