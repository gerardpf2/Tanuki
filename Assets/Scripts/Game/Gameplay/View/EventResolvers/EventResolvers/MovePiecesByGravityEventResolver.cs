using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Actions;
using Game.Gameplay.View.Actions.Actions;
using Infrastructure.System;
using Infrastructure.Unity;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.EventResolvers.EventResolvers
{
    public class MovePiecesByGravityEventResolver : IEventResolver<MovePiecesByGravityEvent>
    {
        private const float SecondsBetweenActions = 0.05f;
        private const float SecondsBetweenActionBatches = SecondsBetweenActions - 0.001f;

        [NotNull] private readonly IBoard _board;
        [NotNull] private readonly IActionFactory _actionFactory;
        [NotNull] private readonly IEventResolverFactory _eventResolverFactory;
        [NotNull] private readonly ICoroutineRunner _coroutineRunner;

        [NotNull] private readonly YieldInstruction _waitForSecondsBetweenActions = new WaitForSeconds(SecondsBetweenActions);
        [NotNull] private readonly YieldInstruction _waitForSecondsBetweenActionBatches = new WaitForSeconds(SecondsBetweenActionBatches);

        public MovePiecesByGravityEventResolver(
            [NotNull] IBoard board,
            [NotNull] IActionFactory actionFactory,
            [NotNull] IEventResolverFactory eventResolverFactory,
            [NotNull] ICoroutineRunner coroutineRunner)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(actionFactory);
            ArgumentNullException.ThrowIfNull(eventResolverFactory);
            ArgumentNullException.ThrowIfNull(coroutineRunner);

            _board = board;
            _actionFactory = actionFactory;
            _eventResolverFactory = eventResolverFactory;
            _coroutineRunner = coroutineRunner;
        }

        public void Resolve([NotNull] MovePiecesByGravityEvent evt, Action onComplete)
        {
            ArgumentNullException.ThrowIfNull(evt);

            IEnumerable<MovePieceEvent> movePieceEvents = GetMovePieceEventsSortedByColumnThenByRow(evt.MovePieceEvents);
            IReadOnlyCollection<MovePieceEvent> movePieceEventsList = new List<MovePieceEvent>(movePieceEvents);

            if (movePieceEventsList.Count <= 0)
            {
                onComplete?.Invoke();
            }
            else
            {
                _coroutineRunner.Run(ResolveImpl(movePieceEventsList, onComplete));
            }
        }

        [NotNull, ItemNotNull]
        private IEnumerable<MovePieceEvent> GetMovePieceEventsSortedByColumnThenByRow(
            [NotNull, ItemNotNull] IEnumerable<MovePieceEvent> movePieceEvents)
        {
            ArgumentNullException.ThrowIfNull(movePieceEvents);

            return movePieceEvents.OrderBy(GetPieceColumn).ThenBy(GetPieceRow);

            int GetPieceRow([NotNull] MovePieceEvent movePieceEvent)
            {
                ArgumentNullException.ThrowIfNull(movePieceEvent);

                return _board.GetSourceCoordinate(movePieceEvent.PieceId).Row;
            }

            int GetPieceColumn([NotNull] MovePieceEvent movePieceEvent)
            {
                ArgumentNullException.ThrowIfNull(movePieceEvent);

                return _board.GetSourceCoordinate(movePieceEvent.PieceId).Column;
            }
        }

        private IEnumerator ResolveImpl(
            [NotNull, ItemNotNull] IReadOnlyCollection<MovePieceEvent> movePieceEvents,
            Action onComplete)
        {
            ArgumentNullException.ThrowIfNull(movePieceEvents);

            int piecesToMove = movePieceEvents.Count;
            ActionGroupCompletionHandler actionGroupCompletionHandler = new(piecesToMove, onComplete);
            ICollection<int> movedPieceIds = new HashSet<int>();

            while (movedPieceIds.Count < piecesToMove)
            {
                ICollection<IAction> movePieceActions = new List<IAction>();

                foreach (MovePieceEvent movePieceEvent in movePieceEvents)
                {
                    ArgumentNullException.ThrowIfNull(movePieceEvent);

                    int pieceId = movePieceEvent.PieceId;

                    if (movedPieceIds.Contains(pieceId) || !CanFall(movePieceEvent))
                    {
                        continue;
                    }

                    movedPieceIds.Add(pieceId);

                    movePieceActions.Add(GetMovePieceAction(movePieceEvent));
                }

                if (movePieceActions.Count > 0)
                {
                    // Resolving it in a different coroutine allows the following ones to be resolved in a parallel way

                    _coroutineRunner.Run(ResolveActionsBatch(movePieceActions, actionGroupCompletionHandler));
                }

                if (movedPieceIds.Count < piecesToMove)
                {
                    yield return _waitForSecondsBetweenActionBatches;
                }
            }
        }

        private bool CanFall([NotNull] MovePieceEvent movePieceEvent)
        {
            /*
             *
             * All pieces are going to fall, but not at the same time. The ones that can fall directly to their
             * end position (the one indicated by model) are going to be resolved, creating empty spaces for the
             * following ones. Each piece falls just once
             *
             */

            ArgumentNullException.ThrowIfNull(movePieceEvent);

            int pieceId = movePieceEvent.PieceId;
            int fall = -movePieceEvent.RowOffset;

            IPiece piece = _board.GetPiece(pieceId);
            Coordinate sourceCoordinate = _board.GetSourceCoordinate(pieceId);

            return _board.ComputePieceFall(piece, sourceCoordinate) == fall;
        }

        [NotNull]
        private IAction GetMovePieceAction(MovePieceEvent movePieceEvent)
        {
            return
                _actionFactory.GetEventResolverAction(
                    _eventResolverFactory.GetMovePieceEventResolver(),
                    movePieceEvent
                );
        }

        private IEnumerator ResolveActionsBatch(
            [NotNull, ItemNotNull] IEnumerable<IAction> actions,
            [NotNull] ActionGroupCompletionHandler actionGroupCompletionHandler)
        {
            ArgumentNullException.ThrowIfNull(actions);
            ArgumentNullException.ThrowIfNull(actionGroupCompletionHandler);

            foreach (IAction action in actions)
            {
                ArgumentNullException.ThrowIfNull(action);

                action.Resolve(actionGroupCompletionHandler.RegisterCompleted);

                yield return _waitForSecondsBetweenActions;
            }
        }
    }
}