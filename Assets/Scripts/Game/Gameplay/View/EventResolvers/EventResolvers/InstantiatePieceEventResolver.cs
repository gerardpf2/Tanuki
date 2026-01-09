using System.Collections.Generic;
using Game.Gameplay.Events.Events;
using Game.Gameplay.View.Actions;
using Game.Gameplay.View.Actions.Actions;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolvers.EventResolvers
{
    public class InstantiatePieceEventResolver : EventResolver<InstantiatePieceEvent>
    {
        [NotNull] private readonly IActionFactory _actionFactory;

        public InstantiatePieceEventResolver([NotNull] IActionFactory actionFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);

            _actionFactory = actionFactory;
        }

        protected override IEnumerable<IAction> GetActions([NotNull] InstantiatePieceEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            yield return
                _actionFactory.GetInstantiatePieceAction(
                    evt.Piece,
                    evt.InstantiatePieceReason,
                    evt.SourceCoordinate
                );
        }
    }
}