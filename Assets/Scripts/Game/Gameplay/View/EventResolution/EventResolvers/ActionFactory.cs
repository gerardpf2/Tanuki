using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing.Events.Reasons;
using Game.Gameplay.View.Animation.Movement;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Camera;
using Game.Gameplay.View.EventResolution.EventResolvers.Actions;
using Game.Gameplay.View.Header.Goals;
using Game.Gameplay.View.Header.Moves;
using Game.Gameplay.View.Player;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolution.EventResolvers
{
    public class ActionFactory : IActionFactory
    {
        [NotNull] private readonly IMovementHelper _movementHelper;
        [NotNull] private readonly IPieceViewDefinitionGetter _pieceViewDefinitionGetter;
        [NotNull] private readonly IBoardView _boardView;
        [NotNull] private readonly ICameraView _cameraView;
        [NotNull] private readonly IGoalsView _goalsView;
        [NotNull] private readonly IMovesView _movesView;
        [NotNull] private readonly IPlayerPieceView _playerPieceView;
        [NotNull] private readonly ICoroutineRunner _coroutineRunner;

        public ActionFactory(
            [NotNull] IMovementHelper movementHelper,
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IBoardView boardView,
            [NotNull] ICameraView cameraView,
            [NotNull] IGoalsView goalsView,
            [NotNull] IMovesView movesView,
            [NotNull] IPlayerPieceView playerPieceView,
            [NotNull] ICoroutineRunner coroutineRunner)
        {
            ArgumentNullException.ThrowIfNull(movementHelper);
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);
            ArgumentNullException.ThrowIfNull(boardView);
            ArgumentNullException.ThrowIfNull(cameraView);
            ArgumentNullException.ThrowIfNull(goalsView);
            ArgumentNullException.ThrowIfNull(movesView);
            ArgumentNullException.ThrowIfNull(playerPieceView);
            ArgumentNullException.ThrowIfNull(coroutineRunner);

            _movementHelper = movementHelper;
            _pieceViewDefinitionGetter = pieceViewDefinitionGetter;
            _boardView = boardView;
            _cameraView = cameraView;
            _goalsView = goalsView;
            _movesView = movesView;
            _playerPieceView = playerPieceView;
            _coroutineRunner = coroutineRunner;
        }

        public IAction GetInstantiatePieceAction(
            [NotNull] IPiece piece,
            InstantiatePieceReason instantiatePieceReason,
            Coordinate sourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(piece);

            return
                new InstantiatePieceAction(
                    _pieceViewDefinitionGetter,
                    piece,
                    instantiatePieceReason,
                    _boardView,
                    sourceCoordinate
                );
        }

        public IAction GetInstantiatePlayerPieceAction(
            [NotNull] IPiece piece,
            InstantiatePieceReason instantiatePieceReason)
        {
            ArgumentNullException.ThrowIfNull(piece);

            return
                new InstantiatePlayerPieceAction(
                    _pieceViewDefinitionGetter,
                    piece,
                    instantiatePieceReason,
                    _playerPieceView
                );
        }

        public IAction GetDestroyPlayerPieceAction(DestroyPieceReason destroyPieceReason)
        {
            return new DestroyPlayerPieceAction(destroyPieceReason, _playerPieceView);
        }

        public IAction GetDamagePieceAction(
            int pieceId,
            IEnumerable<KeyValuePair<string, string>> state,
            DamagePieceReason damagePieceReason)
        {
            return new DamagePieceAction(pieceId, state, damagePieceReason, _boardView);
        }

        public IAction GetDestroyPieceAction(int pieceId, DestroyPieceReason destroyPieceReason)
        {
            return new DestroyPieceAction(destroyPieceReason, pieceId, _boardView);
        }

        public IAction GetMovePieceAction(int pieceId, int rowOffset, int columnOffset, MovePieceReason movePieceReason)
        {
            return new MovePieceAction(_movementHelper, rowOffset, columnOffset, movePieceReason, _boardView, pieceId);
        }

        public IAction GetMovePlayerPieceAction(int rowOffset, int columnOffset, MovePieceReason movePieceReason)
        {
            return new MovePlayerPieceAction(_movementHelper, rowOffset, columnOffset, movePieceReason, _playerPieceView);
        }

        public IAction GetMoveCameraAction(int rowOffset)
        {
            return new MoveCameraAction(_movementHelper, _cameraView, rowOffset);
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
    }
}