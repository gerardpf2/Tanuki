using System.Collections.Generic;
using Game.Common.Pieces;
using Game.Gameplay.Board;
using Game.Gameplay.Pieces;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity.Pooling;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Pieces
{
    public class PieceGameObjectPreloader : IPieceGameObjectPreloader
    {
        [NotNull] private readonly IBoardContainer _boardContainer;
        [NotNull] private readonly IPieceViewDefinitionGetter _pieceViewDefinitionGetter;
        [NotNull] private readonly IGameObjectPool _gameObjectPool;

        public PieceGameObjectPreloader(
            [NotNull] IBoardContainer boardContainer,
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IGameObjectPool gameObjectPool)
        {
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);
            ArgumentNullException.ThrowIfNull(gameObjectPool);

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

            IEnumerable<PiecePlacement> piecePlacements = _boardContainer.PiecePlacements;

            InvalidOperationException.ThrowIfNull(piecePlacements);

            IDictionary<PieceType, int> amountByPieceType = new Dictionary<PieceType, int>();

            foreach (PiecePlacement piecePlacement in piecePlacements)
            {
                InvalidOperationException.ThrowIfNull(piecePlacement);

                PieceType pieceType = piecePlacement.Piece.Type;

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
            // TODO
        }
    }
}