using System;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.Camera;
using Game.Gameplay.Events;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Goals;
using Game.Gameplay.Goals.Utils;
using Game.Gameplay.Pieces.Pieces;
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

        public DestroyNotAlivePiecesPhase(
            [NotNull] IBoard board,
            [NotNull] ICamera camera,
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IEventFactory eventFactory,
            [NotNull] IGoals goals)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(camera);
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);
            ArgumentNullException.ThrowIfNull(goals);

            _board = board;
            _camera = camera;
            _eventEnqueuer = eventEnqueuer;
            _eventFactory = eventFactory;
            _goals = goals;
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

            DestroyPieceEvent.GoalCurrentAmountUpdatedData goalData = IncreaseGoalCurrentAmount(piece);

            _board.RemovePiece(pieceId);

            InstantiateDecomposedBlocks();

            _eventEnqueuer.Enqueue(_eventFactory.GetDestroyPieceEvent(pieceId, DestroyPieceReason.NotAlive, goalData));

            return true;
        }

        private DestroyPieceEvent.GoalCurrentAmountUpdatedData IncreaseGoalCurrentAmount([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            if (!_goals.TryIncreaseCurrentAmount(piece.Type, out int goalCurrentAmount))
            {
                return null;
            }

            Coordinate sourceCoordinate = _board.GetSourceCoordinate(piece.Id);

            return
                new DestroyPieceEvent.GoalCurrentAmountUpdatedData(
                    piece.Type,
                    goalCurrentAmount,
                    sourceCoordinate // TODO: Use center coordinate instead ¿?
                );
        }

        private void InstantiateDecomposedBlocks()
        {
            // TODO
        }
    }
}