using System.Collections.Generic;
using Game.Common.Pieces;
using Game.Gameplay.Bag;
using Game.Gameplay.Board;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity.Pooling;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Pieces.Preloader
{
    public class PieceGameObjectPreloader : IPieceGameObjectPreloader
    {
        [NotNull] private readonly IBag _bag;
        [NotNull] private readonly IBoard _board;
        [NotNull] private readonly IPieceViewDefinitionGetter _pieceViewDefinitionGetter;
        [NotNull] private readonly IGameObjectPool _gameObjectPool;

        public PieceGameObjectPreloader(
            [NotNull] IBag bag,
            [NotNull] IBoard board,
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IGameObjectPool gameObjectPool)
        {
            ArgumentNullException.ThrowIfNull(bag);
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);
            ArgumentNullException.ThrowIfNull(gameObjectPool);

            _bag = bag;
            _board = board;
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

            IDictionary<PieceType, int> amountByPieceType = new Dictionary<PieceType, int>();

            foreach (int pieceId in _board.PieceIds)
            {
                IPiece piece = _board.GetPiece(pieceId);
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
            ICollection<PieceType> pieceTypes = new HashSet<PieceType>();

            foreach (BagPieceEntry bagPieceEntry in _bag.BagPieceEntries)
            {
                PreloadIfNeeded(bagPieceEntry.PieceType);
            }

            foreach (PieceType pieceType in _bag.InitialPieceTypes)
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