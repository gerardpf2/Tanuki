using System;
using Game.Common.Pieces;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.Camera;
using Game.Gameplay.Events;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Goals;
using Game.Gameplay.Goals.Utils;
using Game.Gameplay.Pieces;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.Phases.Phases
{
    public class DestroyNotAlivePiecesPhase : Phase
    {
        [NotNull] private readonly IBoard _board;
        [NotNull] private readonly ICamera _camera;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;
        [NotNull] private readonly IGoals _goals;
        [NotNull] private readonly IPieceGetter _pieceGetter;

        public DestroyNotAlivePiecesPhase(
            [NotNull] IBoard board,
            [NotNull] ICamera camera,
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IEventFactory eventFactory,
            [NotNull] IGoals goals,
            [NotNull] IPieceGetter pieceGetter)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(camera);
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);
            ArgumentNullException.ThrowIfNull(goals);
            ArgumentNullException.ThrowIfNull(pieceGetter);

            _board = board;
            _camera = camera;
            _eventEnqueuer = eventEnqueuer;
            _eventFactory = eventFactory;
            _goals = goals;
            _pieceGetter = pieceGetter;
        }

        protected override ResolveResult ResolveImpl(ResolveContext _)
        {
            bool resolved = false;

            int bottomRow = _camera.BottomRow;
            int topRow = Math.Min(_board.HighestNonEmptyRow, _camera.TopRow);

            foreach (int pieceId in _board.GetDistinctPieceIdsSortedByRowThenByColumn(bottomRow, topRow))
            {
                if (TryDestroyPiece(pieceId))
                {
                    resolved = true;
                }
            }

            return resolved ? ResolveResult.Updated : ResolveResult.NotUpdated;
        }

        private bool TryDestroyPiece(int pieceId)
        {
            IPiece piece = _board.GetPiece(pieceId);

            if (piece.Alive)
            {
                return false;
            }

            Coordinate sourceCoordinate = _board.GetSourceCoordinate(piece.Id);

            DestroyPieceEvent.GoalCurrentAmountUpdatedData goalData =
                IncreaseGoalCurrentAmount(
                    piece.Type,
                    sourceCoordinate
                );

            _board.RemovePiece(pieceId);

            DestroyPieceEvent.DecomposePieceData decomposeData = DecomposePiece(piece, sourceCoordinate);

            _eventEnqueuer.Enqueue(
                _eventFactory.GetDestroyPieceEvent(
                    pieceId,
                    DestroyPieceReason.NotAlive,
                    goalData,
                    decomposeData
                )
            );

            return true;
        }

        private DestroyPieceEvent.GoalCurrentAmountUpdatedData IncreaseGoalCurrentAmount(
            PieceType pieceType,
            Coordinate sourceCoordinate)
        {
            if (!_goals.TryIncreaseCurrentAmount(pieceType, out int goalCurrentAmount))
            {
                return null;
            }

            return
                new DestroyPieceEvent.GoalCurrentAmountUpdatedData(
                    pieceType,
                    goalCurrentAmount,
                    sourceCoordinate // TODO: Use center coordinate instead ¿?
                );
        }

        private DestroyPieceEvent.DecomposePieceData DecomposePiece(
            [NotNull] IPiece pieceToDecompose,
            Coordinate pieceToDecomposeSourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(pieceToDecompose);

            if (!pieceToDecompose.DecomposeType.HasValue)
            {
                return null;
            }

            DestroyPieceEvent.DecomposePieceData decomposeData = new();
            PieceType decomposeType = pieceToDecompose.DecomposeType.Value;
            bool anyAdded = false;

            foreach (Coordinate coordinate in pieceToDecompose.GetUndamagedCoordinates(pieceToDecomposeSourceCoordinate))
            {
                IPiece piece = _pieceGetter.Get(decomposeType);

                _board.AddPiece(piece, coordinate);

                decomposeData.Add(piece, coordinate);

                anyAdded = true;
            }

            return anyAdded ? decomposeData : null;
        }
    }
}