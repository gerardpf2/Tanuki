using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing.Events.Reasons;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Camera;
using Game.Gameplay.View.EventResolution.EventResolvers.Actions;
using Game.Gameplay.View.Header.Goals;
using Game.Gameplay.View.Player;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolution.EventResolvers
{
    public class ActionFactory : IActionFactory
    {
        [NotNull] private readonly IPieceViewDefinitionGetter _pieceViewDefinitionGetter;
        [NotNull] private readonly IBoardView _boardView;
        [NotNull] private readonly ICameraView _cameraView;
        [NotNull] private readonly IGoalsView _goalsView;
        [NotNull] private readonly IPiecePlayerView _piecePlayerView;

        public ActionFactory(
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IBoardView boardView,
            [NotNull] ICameraView cameraView,
            [NotNull] IGoalsView goalsView,
            [NotNull] IPiecePlayerView piecePlayerView)
        {
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);
            ArgumentNullException.ThrowIfNull(boardView);
            ArgumentNullException.ThrowIfNull(cameraView);
            ArgumentNullException.ThrowIfNull(goalsView);
            ArgumentNullException.ThrowIfNull(piecePlayerView);

            _pieceViewDefinitionGetter = pieceViewDefinitionGetter;
            _boardView = boardView;
            _cameraView = cameraView;
            _goalsView = goalsView;
            _piecePlayerView = piecePlayerView;
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
                    _piecePlayerView
                );
        }

        public IAction GetDestroyPlayerPieceAction(DestroyPieceReason destroyPieceReason)
        {
            return new DestroyPlayerPieceAction(destroyPieceReason, _piecePlayerView);
        }

        public IAction GetDamagePieceAction(
            int id,
            IEnumerable<KeyValuePair<string, string>> state,
            DamagePieceReason damagePieceReason)
        {
            return new DamagePieceAction(id, state, damagePieceReason, _boardView);
        }

        public IAction GetDestroyPieceAction(int id, DestroyPieceReason destroyPieceReason)
        {
            return new DestroyPieceAction(destroyPieceReason, id, _boardView, _goalsView);
        }

        public IAction GetMovePieceAction(int id, int rowOffset, int columnOffset, MovePieceReason movePieceReason)
        {
            return new MovePieceAction(_boardView, id, rowOffset, columnOffset, movePieceReason);
        }

        public IAction GetSetCameraRowAction(int topRow)
        {
            return new SetCameraRowAction(_cameraView, topRow);
        }
    }
}