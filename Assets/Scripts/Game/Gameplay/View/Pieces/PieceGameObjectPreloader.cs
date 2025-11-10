using System.Collections.Generic;
using Game.Common.Pieces;
using Game.Gameplay.Bag;
using Game.Gameplay.Board;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity.Pooling;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Pieces
{
    public class PieceGameObjectPreloader : IPieceGameObjectPreloader
    {
        [NotNull] private readonly IBagContainer _bagContainer;
        [NotNull] private readonly IBoardContainer _boardContainer;
        [NotNull] private readonly IPieceViewDefinitionGetter _pieceViewDefinitionGetter;
        [NotNull] private readonly IGameObjectPool _gameObjectPool;

        public PieceGameObjectPreloader(
            [NotNull] IBagContainer bagContainer,
            [NotNull] IBoardContainer boardContainer,
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IGameObjectPool gameObjectPool)
        {
            ArgumentNullException.ThrowIfNull(bagContainer);
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);
            ArgumentNullException.ThrowIfNull(gameObjectPool);

            _bagContainer = bagContainer;
            _boardContainer = boardContainer;
            _pieceViewDefinitionGetter = pieceViewDefinitionGetter;
            _gameObjectPool = gameObjectPool;
        }

        public void Preload()
        {
            PreloadBoardPieces();
            PreloadPieceGhosts();
        }

        private void PreloadBoardPieces()
        {
            // If piece culling is implemented at some point, this will have to be reviewed

            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            IDictionary<PieceType, int> amountByPieceType = new Dictionary<PieceType, int>();

            foreach (int pieceId in board.PieceIds)
            {
                IPiece piece = board.GetPiece(pieceId);
                PieceType pieceType = piece.Type;

                if (amountByPieceType.TryGetValue(pieceType, out int amount))
                {
                    amountByPieceType[pieceType] = amount + 1;
                }
                else
                {
                    amountByPieceType.Add(pieceType, 1);
                }
            }

            foreach ((PieceType pieceType, int amount) in amountByPieceType)
            {
                IPieceViewDefinition pieceViewDefinition = _pieceViewDefinitionGetter.Get(pieceType);

                _gameObjectPool.Preload(pieceViewDefinition.Prefab, amount, true);
            }
        }

        private void PreloadPieceGhosts()
        {
            IBag bag = _bagContainer.Bag;

            InvalidOperationException.ThrowIfNull(bag);

            ICollection<PieceType> pieceTypes = new HashSet<PieceType>();

            foreach (BagPieceEntry bagPieceEntry in bag.BagPieceEntries)
            {
                PreloadIfNeeded(bagPieceEntry.PieceType);
            }

            foreach (PieceType pieceType in bag.InitialPieceTypes)
            {
                PreloadIfNeeded(pieceType);
            }

            return;

            void PreloadIfNeeded(PieceType pieceType)
            {
                if (pieceTypes.Contains(pieceType))
                {
                    return;
                }

                IPieceViewDefinition pieceViewDefinition = _pieceViewDefinitionGetter.GetGhost(pieceType);

                _gameObjectPool.Preload(pieceViewDefinition.Prefab, 1, true);

                pieceTypes.Add(pieceType);
            }
        }
    }
}