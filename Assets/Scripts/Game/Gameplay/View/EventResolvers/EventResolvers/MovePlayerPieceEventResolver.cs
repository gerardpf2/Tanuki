using System.Collections.Generic;
using Game.Gameplay.Events.Events;
using Game.Gameplay.View.Actions;
using Game.Gameplay.View.Actions.Actions;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolvers.EventResolvers
{
    public class MovePlayerPieceEventResolver : EventResolver<MovePlayerPieceEvent>
    {
        [NotNull] private readonly IActionFactory _actionFactory;

        public MovePlayerPieceEventResolver([NotNull] IActionFactory actionFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);

            _actionFactory = actionFactory;
        }

        protected override IEnumerable<IAction> GetActions([NotNull] MovePlayerPieceEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            yield return _actionFactory.GetMovePlayerPieceAction(evt.RowOffset, evt.ColumnOffset, evt.MovePieceReason);
        }
    }
}