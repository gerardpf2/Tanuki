using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.Events.Events;
using Game.Gameplay.View.Actions;
using Game.Gameplay.View.Actions.Actions;
using Infrastructure.System;
using Infrastructure.Unity;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.EventResolvers.EventResolvers
{
    public class InstantiateInitialPiecesAndMoveCameraEventResolver : IEventResolver<InstantiateInitialPiecesAndMoveCameraEvent>
    {
        private const float SecondsBetweenRowActions = 0.01f;
        private const float SecondsBetweenRows = 0.1f;

        [NotNull] private readonly IActionFactory _actionFactory;
        [NotNull] private readonly IEventResolverFactory _eventResolverFactory;
        [NotNull] private readonly ICoroutineRunner _coroutineRunner;

        public InstantiateInitialPiecesAndMoveCameraEventResolver(
            [NotNull] IActionFactory actionFactory,
            [NotNull] IEventResolverFactory eventResolverFactory,
            [NotNull] ICoroutineRunner coroutineRunner)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);
            ArgumentNullException.ThrowIfNull(eventResolverFactory);
            ArgumentNullException.ThrowIfNull(coroutineRunner);

            _actionFactory = actionFactory;
            _eventResolverFactory = eventResolverFactory;
            _coroutineRunner = coroutineRunner;
        }

        public void Resolve([NotNull] InstantiateInitialPiecesAndMoveCameraEvent evt, Action onComplete)
        {
            ArgumentNullException.ThrowIfNull(evt);

            ActionGroupCompletionHandler actionGroupCompletionHandler = new(2, onComplete);

            RunInstantiateInitialPieces(evt, actionGroupCompletionHandler.RegisterCompleted);
            RunMoveCamera(evt.MoveCameraEvent, actionGroupCompletionHandler.RegisterCompleted);
        }

        private void RunInstantiateInitialPieces(
            [NotNull] InstantiateInitialPiecesAndMoveCameraEvent evt,
            Action onComplete)
        {
            ArgumentNullException.ThrowIfNull(evt);

            _coroutineRunner.Run(InstantiateInitialPieces(evt, onComplete));
        }

        private IEnumerator InstantiateInitialPieces(
            [NotNull] InstantiateInitialPiecesAndMoveCameraEvent evt,
            Action onComplete)
        {
            ArgumentNullException.ThrowIfNull(evt);

            IReadOnlyCollection<IGrouping<int, InstantiatePieceEvent>> instantiatePieceEventsGroupedByRowAndSortedByColumn =
                new List<IGrouping<int, InstantiatePieceEvent>>(
                    GetInstantiatePieceEventsGroupedByRowAndSortedByColumn(
                        evt.InstantiatePieceEvents
                    )
                );

            if (instantiatePieceEventsGroupedByRowAndSortedByColumn.Count <= 0)
            {
                onComplete?.Invoke();

                yield break;
            }

            int prevRow = 0;

            ActionGroupCompletionHandler actionGroupCompletionHandler =
                new(
                    instantiatePieceEventsGroupedByRowAndSortedByColumn.Count,
                    onComplete
                );

            foreach (IGrouping<int, InstantiatePieceEvent> instantiatePieceEvents in instantiatePieceEventsGroupedByRowAndSortedByColumn)
            {
                int row = instantiatePieceEvents.Key;
                int rowOffset = row - prevRow;

                prevRow = row;

                if (rowOffset > 0)
                {
                    yield return new WaitForSeconds(rowOffset * SecondsBetweenRows);
                }

                IEnumerable<IAction> instantiatePieceActions = instantiatePieceEvents.Select(GetInstantiatePieceAction);

                IAction instantiatePiecesParallelActionGroup =
                    _actionFactory.GetParallelActionGroup(
                        instantiatePieceActions,
                        SecondsBetweenRowActions
                    );

                instantiatePiecesParallelActionGroup.Resolve(actionGroupCompletionHandler.RegisterCompleted);
            }
        }

        [NotNull, ItemNotNull] // ItemNotNull for InstantiatePieceEvent too
        private static IEnumerable<IGrouping<int, InstantiatePieceEvent>> GetInstantiatePieceEventsGroupedByRowAndSortedByColumn(
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

        private void RunMoveCamera([NotNull] MoveCameraEvent moveCameraEvent, Action onComplete)
        {
            ArgumentNullException.ThrowIfNull(moveCameraEvent);

            if (moveCameraEvent.RowOffset == 0)
            {
                onComplete?.Invoke();

                return;
            }

            _coroutineRunner.Run(MoveCamera(moveCameraEvent, onComplete));
        }

        private IEnumerator MoveCamera(MoveCameraEvent moveCameraEvent, Action onComplete)
        {
            yield return new WaitForSeconds(SecondsBetweenRows);

            IAction moveCameraAction = GetMoveCameraAction(moveCameraEvent);

            moveCameraAction.Resolve(onComplete);
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