using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.Board;
using Game.Gameplay.Events.Events;
using Game.Gameplay.View.Actions;
using Game.Gameplay.View.Actions.Actions;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolvers.EventResolvers
{
    public class DamagePiecesByLineClearEventResolver : EventResolver<DamagePiecesByLineClearEvent>
    {
        private const float SecondsBetweenColumnActions = 0.01f;
        private const float SecondsBetweenColumns = 0.1f;

        [NotNull] private readonly IBoard _board;
        [NotNull] private readonly IActionFactory _actionFactory;
        [NotNull] private readonly IEventResolverFactory _eventResolverFactory;

        public DamagePiecesByLineClearEventResolver(
            [NotNull] IBoard board,
            [NotNull] IActionFactory actionFactory,
            [NotNull] IEventResolverFactory eventResolverFactory)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(actionFactory);
            ArgumentNullException.ThrowIfNull(eventResolverFactory);

            _board = board;
            _actionFactory = actionFactory;
            _eventResolverFactory = eventResolverFactory;
        }

        protected override IEnumerable<IAction> GetActions([NotNull] DamagePiecesByLineClearEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            IEnumerable<IEnumerable<DamagePieceEvent>> damagePieceEventsGroupedByColumnAndSortedByRow =
                GetDamagePieceEventsGroupedByColumnAndSortedByRow(
                    evt.DamagePieceEvents
                );

            Queue<IAction> actions = new();

            foreach (IEnumerable<DamagePieceEvent> damagePieceEvents in damagePieceEventsGroupedByColumnAndSortedByRow)
            {
                IEnumerable<IAction> damagePieceActions = damagePieceEvents.Select(GetDamagePieceAction);

                actions.Enqueue(_actionFactory.GetParallelActionGroup(damagePieceActions, SecondsBetweenColumnActions));
            }

            yield return _actionFactory.GetParallelActionGroup(actions, SecondsBetweenColumns);
        }

        [NotNull, ItemNotNull] // ItemNotNull for DamagePieceEvent too
        private IEnumerable<IEnumerable<DamagePieceEvent>> GetDamagePieceEventsGroupedByColumnAndSortedByRow(
            [NotNull, ItemNotNull] IEnumerable<DamagePieceEvent> damagePieceEvents)
        {
            ArgumentNullException.ThrowIfNull(damagePieceEvents);

            return damagePieceEvents.OrderBy(GetPieceColumn).ThenBy(GetPieceRow).GroupBy(GetPieceColumn);

            int GetPieceRow([NotNull] DamagePieceEvent damagePieceEvent)
            {
                ArgumentNullException.ThrowIfNull(damagePieceEvent);

                return _board.GetSourceCoordinate(damagePieceEvent.PieceId).Row;
            }

            int GetPieceColumn([NotNull] DamagePieceEvent damagePieceEvent)
            {
                ArgumentNullException.ThrowIfNull(damagePieceEvent);

                return _board.GetSourceCoordinate(damagePieceEvent.PieceId).Column;
            }
        }

        [NotNull]
        private IAction GetDamagePieceAction(DamagePieceEvent damagePieceEvent)
        {
            return
                _actionFactory.GetEventResolverAction(
                    _eventResolverFactory.GetDamagePieceEventResolver(),
                    damagePieceEvent
                );
        }
    }
}