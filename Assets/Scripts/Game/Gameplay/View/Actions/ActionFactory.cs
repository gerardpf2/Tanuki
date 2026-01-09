using System.Collections.Generic;
using Game.Common;
using Game.Common.Pieces;
using Game.Gameplay.Board;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Actions.Actions;
using Game.Gameplay.View.Animation.Movement;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Camera;
using Game.Gameplay.View.EventResolvers.EventResolvers;
using Game.Gameplay.View.Goals;
using Game.Gameplay.View.Moves;
using Game.Gameplay.View.Player;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Actions
{
    public class ActionFactory : IActionFactory
    {
        [NotNull] private readonly IBoard _board;
        [NotNull] private readonly IMovementHelper _movementHelper;
        [NotNull] private readonly IBoardView _boardView;
        [NotNull] private readonly ICameraView _cameraView;
        [NotNull] private readonly IGoalsView _goalsView;
        [NotNull] private readonly IMovesView _movesView;
        [NotNull] private readonly IPlayerPieceGhostView _playerPieceGhostView;
        [NotNull] private readonly IPlayerPieceView _playerPieceView;
        [NotNull] private readonly ICoroutineRunner _coroutineRunner;

        public ActionFactory(
            [NotNull] IBoard board,
            [NotNull] IMovementHelper movementHelper,
            [NotNull] IBoardView boardView,
            [NotNull] ICameraView cameraView,
            [NotNull] IGoalsView goalsView,
            [NotNull] IMovesView movesView,
            [NotNull] IPlayerPieceGhostView playerPieceGhostView,
            [NotNull] IPlayerPieceView playerPieceView,
            [NotNull] ICoroutineRunner coroutineRunner)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(movementHelper);
            ArgumentNullException.ThrowIfNull(boardView);
            ArgumentNullException.ThrowIfNull(cameraView);
            ArgumentNullException.ThrowIfNull(goalsView);
            ArgumentNullException.ThrowIfNull(movesView);
            ArgumentNullException.ThrowIfNull(playerPieceGhostView);
            ArgumentNullException.ThrowIfNull(playerPieceView);
            ArgumentNullException.ThrowIfNull(coroutineRunner);

            _board = board;
            _movementHelper = movementHelper;
            _boardView = boardView;
            _cameraView = cameraView;
            _goalsView = goalsView;
            _movesView = movesView;
            _playerPieceGhostView = playerPieceGhostView;
            _playerPieceView = playerPieceView;
            _coroutineRunner = coroutineRunner;
        }

        public IAction GetInstantiatePieceAction(
            [NotNull] IPiece piece,
            InstantiatePieceReason instantiatePieceReason,
            Coordinate sourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(piece);

            return new InstantiatePieceAction(piece, instantiatePieceReason, _boardView, sourceCoordinate);
        }

        public IAction GetInstantiatePlayerPieceAction(
            [NotNull] IPiece piece,
            InstantiatePieceReason instantiatePieceReason,
            Coordinate sourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(piece);

            return new InstantiatePlayerPieceAction(piece, instantiatePieceReason, _playerPieceView, sourceCoordinate);
        }

        public IAction GetInstantiatePlayerPieceGhostAction(
            [NotNull] IPiece piece,
            InstantiatePieceReason instantiatePieceReason)
        {
            ArgumentNullException.ThrowIfNull(piece);

            return new InstantiatePlayerPieceGhostAction(piece, instantiatePieceReason, _playerPieceGhostView);
        }

        public IAction GetDestroyPieceAction(int pieceId, DestroyPieceReason destroyPieceReason)
        {
            return new DestroyPieceAction(destroyPieceReason, pieceId, _boardView);
        }

        public IAction GetDestroyPlayerPieceAction(DestroyPieceReason destroyPieceReason)
        {
            return new DestroyPlayerPieceAction(destroyPieceReason, _playerPieceView);
        }

        public IAction GetDestroyPlayerPieceGhostAction(DestroyPieceReason destroyPieceReason)
        {
            return new DestroyPlayerPieceGhostAction(destroyPieceReason, _playerPieceGhostView);
        }

        public IAction GetDamagePieceAction(
            int pieceId,
            IEnumerable<KeyValuePair<string, string>> state,
            DamagePieceReason damagePieceReason,
            Direction direction)
        {
            return new DamagePieceAction(pieceId, state, damagePieceReason, direction, _boardView);
        }

        public IAction GetMovePieceAction(int pieceId, int rowOffset, int columnOffset, MovePieceReason movePieceReason)
        {
            return
                new MovePieceAction(
                    _board,
                    _movementHelper,
                    _boardView,
                    rowOffset,
                    columnOffset,
                    movePieceReason,
                    pieceId
                );
        }

        public IAction GetMovePlayerPieceAction(int rowOffset, int columnOffset, MovePieceReason movePieceReason)
        {
            return
                new MovePlayerPieceAction(
                    _board,
                    _movementHelper,
                    _boardView,
                    rowOffset,
                    columnOffset,
                    movePieceReason,
                    _playerPieceView
                );
        }

        public IAction GetMoveCameraAction(int rowOffset, MoveCameraReason moveCameraReason)
        {
            return new MoveCameraAction(_movementHelper, _cameraView, rowOffset, moveCameraReason);
        }

        public IAction GetSetGoalCurrentAmountAction(PieceType pieceType, int currentAmount, Coordinate coordinate)
        {
            return new SetGoalCurrentAmountAction(_goalsView, pieceType, currentAmount, coordinate);
        }

        public IAction GetSetMovesAmountAction(int amount)
        {
            return new SetMovesAmountAction(_movesView, amount);
        }

        public IAction GetParallelActionGroup(
            [NotNull, ItemNotNull] IEnumerable<IAction> actions,
            float secondsBetweenActions)
        {
            ArgumentNullException.ThrowIfNull(actions);

            ParallelActionGroup parallelActionGroup = new(_coroutineRunner, secondsBetweenActions);

            foreach (IAction action in actions)
            {
                ArgumentNullException.ThrowIfNull(action);

                parallelActionGroup.Add(action);
            }

            return parallelActionGroup;
        }

        public IAction GetEventResolverAction<TEvent>([NotNull] IEventResolver<TEvent> eventResolver, TEvent evt) where TEvent : IEvent
        {
            ArgumentNullException.ThrowIfNull(eventResolver);

            return new EventResolverAction<TEvent>(eventResolver, evt);
        }
    }
}