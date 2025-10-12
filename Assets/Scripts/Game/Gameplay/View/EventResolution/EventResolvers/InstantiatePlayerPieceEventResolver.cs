using System.Collections.Generic;
using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.EventEnqueueing.Events.Reasons;
using Game.Gameplay.View.EventResolution.EventResolvers.Actions;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolution.EventResolvers
{
    public class InstantiatePlayerPieceEventResolver : EventResolver<InstantiatePlayerPieceEvent>
    {
        [NotNull] private readonly IActionFactory _actionFactory;

        public InstantiatePlayerPieceEventResolver([NotNull] IActionFactory actionFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);

            _actionFactory = actionFactory;
        }

        protected override IEnumerable<IAction> GetActions([NotNull] InstantiatePlayerPieceEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            yield return _actionFactory.GetInstantiatePlayerPieceAction(evt.Piece, InstantiatePieceReason.Player);
        }
    }
}