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

        [NotNull] private readonly IDictionary<PieceType, int> _amountByPieceType = new Dictionary<PieceType, int>();

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
            _amountByPieceType.Clear();

            AddBoardAmounts();
            AddBagAmounts();
            PreloadImpl();
        }

        private void AddBoardAmounts()
        {
            IEnumerable<PiecePlacement> piecePlacements = _boardContainer.PiecePlacements;

            InvalidOperationException.ThrowIfNull(piecePlacements);

            foreach (PiecePlacement piecePlacement in piecePlacements)
            {
                InvalidOperationException.ThrowIfNull(piecePlacement);

                IncreaseAmount(piecePlacement.Piece.Type);
            }
        }

        private void AddBagAmounts()
        {
            // TODO: Remove if not needed
        }

        private void IncreaseAmount(PieceType pieceType)
        {
            if (_amountByPieceType.TryGetValue(pieceType, out int amount))
            {
                _amountByPieceType[pieceType] = amount + 1;
            }
            else
            {
                _amountByPieceType.Add(pieceType, 1);
            }
        }

        private void PreloadImpl()
        {
            foreach ((PieceType pieceType, int amount) in _amountByPieceType)
            {
                IPieceViewDefinition pieceViewDefinition = _pieceViewDefinitionGetter.Get(pieceType);

                _gameObjectPool.Preload(pieceViewDefinition.Prefab, amount, true);
            }
        }
    }
}