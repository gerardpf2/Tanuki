using Game.Common.Pieces;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Pieces;
using Game.Gameplay.View.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity.Pooling;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Player
{
    public class PlayerPieceGhostView : BasePlayerPieceView, IPlayerPieceGhostView
    {
        [NotNull] private readonly IBoard _board;
        [NotNull] private readonly IPlayerPieceView _playerPieceView;

        protected override string ParentName => "PlayerPieceGhostParent";

        public PlayerPieceGhostView(
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IGameObjectPool gameObjectPool,
            [NotNull] IBoard board,
            [NotNull] IPlayerPieceView playerPieceView) : base(pieceViewDefinitionGetter, gameObjectPool)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(playerPieceView);

            _board = board;
            _playerPieceView = playerPieceView;
        }

        public override void Initialize()
        {
            base.Initialize();

            SubscribeToEvents();
        }

        public override void Uninitialize()
        {
            base.Uninitialize();

            UnsubscribeFromEvents();
        }

        public void Instantiate([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            InstantiateImpl(piece, GetLockSourceCoordinate(piece));
        }

        protected override IPieceViewDefinition GetPieceViewDefinition(
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            PieceType pieceType)
        {
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);

            return pieceViewDefinitionGetter.GetPlayerPieceGhost(pieceType);
        }

        private void SubscribeToEvents()
        {
            UnsubscribeFromEvents();

            _playerPieceView.OnMoved += HandleMoved;
            _playerPieceView.OnRotated += HandleRotated;
        }

        private void UnsubscribeFromEvents()
        {
            _playerPieceView.OnMoved -= HandleMoved;
            _playerPieceView.OnRotated -= HandleRotated;
        }

        private Coordinate GetLockSourceCoordinate([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            Coordinate sourceCoordinate = _playerPieceView.Coordinate;
            Coordinate lockSourceCoordinate = _board.GetPieceLockSourceCoordinate(piece, sourceCoordinate);

            return lockSourceCoordinate;
        }

        private void HandleMoved()
        {
            IPiece piece = Piece;

            InvalidOperationException.ThrowIfNull(piece);

            Coordinate = GetLockSourceCoordinate(piece);
        }

        private void HandleRotated()
        {
            GameObject instance = Instance;

            InvalidOperationException.ThrowIfNull(instance);

            IPieceViewEventNotifier pieceViewEventNotifier = instance.GetComponent<IPieceViewEventNotifier>();

            InvalidOperationException.ThrowIfNull(pieceViewEventNotifier);

            pieceViewEventNotifier.OnRotated();
        }
    }
}