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
using Game.Gameplay.Pieces.Pieces;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.Phases.Phases
{
    public class DestroyNotAlivePiecesPhase : Phase
    {
        [NotNull] private readonly IBoard _board;
        [NotNull] private readonly ICamera _camera;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;
        [NotNull] private readonly IGoalsContainer _goalsContainer;

        public DestroyNotAlivePiecesPhase(
            [NotNull] IBoard board,
            [NotNull] ICamera camera,
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IEventFactory eventFactory,
            [NotNull] IGoalsContainer goalsContainer)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(camera);
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);
            ArgumentNullException.ThrowIfNull(goalsContainer);

            _board = board;
            _camera = camera;
            _eventEnqueuer = eventEnqueuer;
            _eventFactory = eventFactory;
            _goalsContainer = goalsContainer;
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

            DestroyPieceEvent.GoalCurrentAmountUpdatedData goalData = null;

            if (TryIncreaseGoalCurrentAmount(piece.Type, out int goalCurrentAmount))
            {
                Coordinate sourceCoordinate = _board.GetSourceCoordinate(pieceId);

                goalData = new DestroyPieceEvent.GoalCurrentAmountUpdatedData(
                    piece.Type,
                    goalCurrentAmount,
                    sourceCoordinate // TODO: Use center coordinate instead ¿?
                );
            }

            _board.RemovePiece(pieceId);

            _eventEnqueuer.Enqueue(_eventFactory.GetDestroyPieceEvent(pieceId, DestroyPieceReason.NotAlive, goalData));

            return true;
        }

        private bool TryIncreaseGoalCurrentAmount(PieceType pieceType, out int goalCurrentAmount)
        {
            IGoals goals = _goalsContainer.Goals;

            InvalidOperationException.ThrowIfNull(goals);

            return goals.TryIncreaseCurrentAmount(pieceType, out goalCurrentAmount);
        }
    }
}