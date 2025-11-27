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
    public class DecomposePiecesGameObjectPreloader : BasePieceGameObjectPreloader
    {
        [NotNull] private readonly IBoard _board;

        public DecomposePiecesGameObjectPreloader(
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

            const int amount = 3;

            foreach (int pieceId in _board.PieceIds)
            {
                IPiece piece = _board.GetPiece(pieceId);

                if (!piece.DecomposeType.HasValue)
                {
                    continue;
                }

                PieceType decomposeType = piece.DecomposeType.Value;

                GameObject prefab = pieceViewDefinitionGetter.GetBoardPiece(decomposeType).Prefab;

                yield return new PreloadRequest(prefab, amount);
            }
        }
    }
}