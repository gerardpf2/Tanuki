using System.Collections.Generic;
using Game.Common.Pieces;
using Game.Gameplay.Board;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity.Pooling;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Pieces.Preloader
{
    public class BoardPiecesGameObjectPreloader : BasePieceGameObjectPreloader
    {
        // If piece culling is implemented at some point, this will have to be reviewed

        [NotNull] private readonly IBoard _board;

        public BoardPiecesGameObjectPreloader(
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IGameObjectPool gameObjectPool,
            [NotNull] IBoard board) : base(pieceViewDefinitionGetter, gameObjectPool)
        {
            ArgumentNullException.ThrowIfNull(board);

            _board = board;
        }

        protected override IEnumerable<PreloadRequest> GetPreloadRequests(
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter)
        {
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);

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
                GameObject prefab = pieceViewDefinitionGetter.Get(pieceType).Prefab;

                yield return new PreloadRequest(prefab, amount);
            }
        }
    }
}