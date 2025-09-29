using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing.Events.Reasons;
using Game.Gameplay.View.Board;
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
        [NotNull] private readonly IGoalsView _goalsView;
        [NotNull] private readonly IPlayerView _playerView;

        public ActionFactory(
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IBoardView boardView,
            [NotNull] IGoalsView goalsView,
            [NotNull] IPlayerView playerView)
        {
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);
            ArgumentNullException.ThrowIfNull(boardView);
            ArgumentNullException.ThrowIfNull(goalsView);
            ArgumentNullException.ThrowIfNull(playerView);

            _pieceViewDefinitionGetter = pieceViewDefinitionGetter;
            _boardView = boardView;
            _goalsView = goalsView;
            _playerView = playerView;
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
                    _playerView
                );
        }

        public IAction GetDestroyPlayerPieceAction(DestroyPieceReason destroyPieceReason)
        {
            return new DestroyPlayerPieceAction(destroyPieceReason, _playerView);
        }

        public IAction GetDamagePieceAction(IPiece piece)
        {
            return new DamagePieceAction(piece, _boardView);
        }

        public IAction GetDestroyPieceAction([NotNull] IPiece piece, DestroyPieceReason destroyPieceReason)
        {
            ArgumentNullException.ThrowIfNull(piece);

            return new DestroyPieceAction(destroyPieceReason, piece, _boardView, _goalsView);
        }

        public IAction GetMovePieceAction(IPiece piece, int rowOffset, int columnOffset, MovePieceReason movePieceReason)
        {
            return new MovePieceAction(_boardView, piece, rowOffset, columnOffset, movePieceReason);
        }
    }
}