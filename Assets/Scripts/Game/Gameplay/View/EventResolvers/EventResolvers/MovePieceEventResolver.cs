using System.Collections.Generic;
using Game.Gameplay.Events.Events;
using Game.Gameplay.View.Actions;
using Game.Gameplay.View.Actions.Actions;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolvers.EventResolvers
{
    public class MovePieceEventResolver : EventResolver<MovePieceEvent>
    {
        [NotNull] private readonly IActionFactory _actionFactory;

        public MovePieceEventResolver([NotNull] IActionFactory actionFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);

            _actionFactory = actionFactory;
        }

        protected override IEnumerable<IAction> GetActions([NotNull] MovePieceEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            yield return
                _actionFactory.GetMovePieceAction(
                    evt.PieceId,
                    evt.RowOffset,
                    evt.ColumnOffset,
                    evt.MovePieceReason
                );
        }
    }
}