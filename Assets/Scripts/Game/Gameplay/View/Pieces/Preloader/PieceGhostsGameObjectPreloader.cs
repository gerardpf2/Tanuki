using System.Collections.Generic;
using Game.Common.Pieces;
using Game.Gameplay.Bag;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity.Pooling;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Pieces.Preloader
{
    public class PieceGhostsGameObjectPreloader : BasePieceGameObjectPreloader
    {
        [NotNull] private readonly IBag _bag;

        public PieceGhostsGameObjectPreloader(
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IGameObjectPool gameObjectPool,
            [NotNull] IBag bag) : base(pieceViewDefinitionGetter, gameObjectPool)
        {
            ArgumentNullException.ThrowIfNull(bag);

            _bag = bag;
        }

        protected override IEnumerable<PreloadRequest> GetPreloadRequests(
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter)
        {
            foreach (BagPieceEntry bagPieceEntry in _bag.BagPieceEntries)
            {
                yield return GetPreloadRequest(bagPieceEntry.PieceType);
            }

            foreach (PieceType pieceType in _bag.InitialPieceTypes)
            {
                yield return GetPreloadRequest(pieceType);
            }

            yield break;

            PreloadRequest GetPreloadRequest(PieceType pieceType)
            {
                GameObject prefab = pieceViewDefinitionGetter.GetGhost(pieceType).Prefab;

                return new PreloadRequest(prefab, 1, true);
            }
        }
    }
}