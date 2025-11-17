using System;
using System.Collections;
using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Actions;
using Game.Gameplay.View.Actions.Actions;
using Infrastructure.System;
using Infrastructure.Unity;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.EventResolvers.EventResolvers
{
    public class MovePiecesByGravityEventResolver : IEventResolver<MovePiecesByGravityEvent>
    {
        private sealed class FallDataComparer : IComparer<int>
        {
            [NotNull] private readonly IBoard _board;

            public FallDataComparer([NotNull] IBoard board)
            {
                ArgumentNullException.ThrowIfNull(board);

                _board = board;
            }

            public int Compare(int pieceIdA, int pieceIdB)
            {
                Coordinate sourceCoordinateA = _board.GetSourceCoordinate(pieceIdA);
                Coordinate sourceCoordinateB = _board.GetSourceCoordinate(pieceIdB);

                int result = sourceCoordinateA.Column.CompareTo(sourceCoordinateB.Column);

                if (result == 0)
                {
                    result = sourceCoordinateA.Row.CompareTo(sourceCoordinateB.Row);
                }

                if (result == 0)
                {
                    result = pieceIdA.CompareTo(pieceIdB);
                }

                return result;
            }
        }

        [NotNull] private readonly IBoard _board;
        [NotNull] private readonly IActionFactory _actionFactory;
        [NotNull] private readonly ICoroutineRunner _coroutineRunner;

        public MovePiecesByGravityEventResolver(
            [NotNull] IBoard board,
            [NotNull] IActionFactory actionFactory,
            [NotNull] ICoroutineRunner coroutineRunner)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(actionFactory);
            ArgumentNullException.ThrowIfNull(coroutineRunner);

            _board = board;
            _actionFactory = actionFactory;
            _coroutineRunner = coroutineRunner;
        }

        public void Resolve([NotNull] MovePiecesByGravityEvent evt, Action onComplete)
        {
            ArgumentNullException.ThrowIfNull(evt);

            IDictionary<int, int> fallData = GetFallData(evt);

            if (fallData.Count <= 0)
            {
                onComplete?.Invoke();
            }
            else
            {
                _coroutineRunner.Run(ResolveImpl(fallData, onComplete));
            }
        }

        [NotNull]
        private IDictionary<int, int> GetFallData([NotNull] MovePiecesByGravityEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            IComparer<int> fallDataComparer = new FallDataComparer(_board);
            IDictionary<int, int> fallData = new SortedDictionary<int, int>(fallDataComparer);

            foreach ((int pieceId, int fall) in evt.FallData)
            {
                if (!fallData.TryAdd(pieceId, fall))
                {
                    InvalidOperationException.Throw(); // TODO
                }
            }

            return fallData;
        }

        private IEnumerator ResolveImpl([NotNull] IDictionary<int, int> fallData, Action onComplete)
        {
            ArgumentNullException.ThrowIfNull(fallData);

            ActionGroupCompletionHandler actionGroupCompletionHandler = new(fallData.Count, onComplete);
            List<int> pieceIds = new();

            while (fallData.Count > 0)
            {
                pieceIds.AddRange(fallData.Keys);

                foreach (int pieceId in pieceIds)
                {
                    int fall = fallData[pieceId];

                    if (!CanFall(pieceId, fall))
                    {
                        continue;
                    }

                    fallData.Remove(pieceId);

                    IAction fallAction = GetFallAction(pieceId, fall);

                    fallAction.Resolve(actionGroupCompletionHandler.RegisterCompleted);

                    yield return new WaitForSeconds(0.05f); // TODO
                }

                pieceIds.Clear();
            }

            yield break;

            bool CanFall(int pieceId, int fall)
            {
                IPiece piece = _board.GetPiece(pieceId);
                Coordinate sourceCoordinate = _board.GetSourceCoordinate(pieceId);

                return _board.ComputePieceFall(piece, sourceCoordinate) == fall;
            }

            [NotNull]
            IAction GetFallAction(int pieceId, int fall)
            {
                const int columnOffset = 0;
                int rowOffset = -fall;

                return _actionFactory.GetMovePieceAction(pieceId, rowOffset, columnOffset, MovePieceReason.Gravity);
            }
        }
    }
}