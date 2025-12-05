using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.Events.Events;
using Game.Gameplay.View.Actions;
using Game.Gameplay.View.Actions.Actions;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolvers.EventResolvers
{
    public class InstantiateInitialPiecesAndMoveCameraEventResolver : EventResolver<InstantiateInitialPiecesAndMoveCameraEvent>
    {
        private const float SecondsBetweenRowActions = 0.01f;
        private const float SecondsBetweenRows = 0.1f;

        [NotNull] private readonly IActionFactory _actionFactory;
        [NotNull] private readonly IEventResolverFactory _eventResolverFactory;

        public InstantiateInitialPiecesAndMoveCameraEventResolver(
            [NotNull] IActionFactory actionFactory,
            [NotNull] IEventResolverFactory eventResolverFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);
            ArgumentNullException.ThrowIfNull(eventResolverFactory);

            _actionFactory = actionFactory;
            _eventResolverFactory = eventResolverFactory;
        }

        protected override IEnumerable<IAction> GetActions([NotNull] InstantiateInitialPiecesAndMoveCameraEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            ICollection<IAction> actions = new List<IAction>();

            IAction instantiateInitialPiecesAction = GetInstantiateInitialPiecesAction(evt);

            actions.Add(instantiateInitialPiecesAction);

            MoveCameraEvent moveCameraEvent = evt.MoveCameraEvent;

            if (moveCameraEvent.RowOffset != 0)
            {
                IAction moveCameraAction = GetMoveCameraAction(moveCameraEvent);

                actions.Add(moveCameraAction);
            }

            yield return _actionFactory.GetParallelActionGroup(actions, SecondsBetweenRows);
        }

        [NotNull]
        private IAction GetInstantiateInitialPiecesAction(
            [NotNull] InstantiateInitialPiecesAndMoveCameraEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            ICollection<IAction> actions = new List<IAction>();

            IEnumerable<IEnumerable<InstantiatePieceEvent>> instantiatePieceEventsGroupedByRowAndSortedByColumn =
                GetInstantiatePieceEventsGroupedByRowAndSortedByColumn(
                    evt.InstantiatePieceEvents
                );

            foreach (IEnumerable<InstantiatePieceEvent> instantiatePieceEvents in instantiatePieceEventsGroupedByRowAndSortedByColumn)
            {
                IEnumerable<IAction> instantiatePieceActions = instantiatePieceEvents.Select(GetInstantiatePieceAction);

                actions.Add(_actionFactory.GetParallelActionGroup(instantiatePieceActions, SecondsBetweenRowActions));
            }

            return _actionFactory.GetParallelActionGroup(actions, SecondsBetweenRows);
        }

        [NotNull, ItemNotNull] // ItemNotNull for InstantiatePieceEvent too
        private static IEnumerable<IEnumerable<InstantiatePieceEvent>> GetInstantiatePieceEventsGroupedByRowAndSortedByColumn(
            [NotNull, ItemNotNull] IEnumerable<InstantiatePieceEvent> instantiatePieceEvents)
        {
            ArgumentNullException.ThrowIfNull(instantiatePieceEvents);

            return instantiatePieceEvents.OrderBy(GetRow).ThenBy(GetColumn).GroupBy(GetRow);

            int GetRow([NotNull] InstantiatePieceEvent instantiatePieceEvent)
            {
                ArgumentNullException.ThrowIfNull(instantiatePieceEvent);

                return instantiatePieceEvent.SourceCoordinate.Row;
            }

            int GetColumn([NotNull] InstantiatePieceEvent instantiatePieceEvent)
            {
                ArgumentNullException.ThrowIfNull(instantiatePieceEvent);

                return instantiatePieceEvent.SourceCoordinate.Column;
            }
        }

        [NotNull]
        private IAction GetInstantiatePieceAction(InstantiatePieceEvent instantiatePieceEvent)
        {
            return
                _actionFactory.GetEventResolverAction(
                    _eventResolverFactory.GetInstantiatePieceEventResolver(),
                    instantiatePieceEvent
                );
        }

        [NotNull]
        private IAction GetMoveCameraAction(MoveCameraEvent moveCameraEvent)
        {
            return
                _actionFactory.GetEventResolverAction(
                    _eventResolverFactory.GetMoveCameraEventResolver(),
                    moveCameraEvent
                );
        }
    }
}