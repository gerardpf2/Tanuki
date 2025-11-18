using System.Collections.Generic;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity.Pooling;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Pieces.Preloader
{
    public abstract class BasePieceGameObjectPreloader : IPieceGameObjectPreloader
    {
        protected readonly struct PreloadRequest
        {
            public readonly GameObject Prefab;
            public readonly int Amount;
            public readonly bool OnlyIfNeeded;

            public PreloadRequest(GameObject prefab, int amount, bool onlyIfNeeded)
            {
                Prefab = prefab;
                Amount = amount;
                OnlyIfNeeded = onlyIfNeeded;
            }
        }

        [NotNull] private readonly IPieceViewDefinitionGetter _pieceViewDefinitionGetter;
        [NotNull] private readonly IGameObjectPool _gameObjectPool;

        protected BasePieceGameObjectPreloader(
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IGameObjectPool gameObjectPool)
        {
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);
            ArgumentNullException.ThrowIfNull(gameObjectPool);

            _pieceViewDefinitionGetter = pieceViewDefinitionGetter;
            _gameObjectPool = gameObjectPool;
        }

        public void Preload()
        {
            foreach (PreloadRequest preloadRequest in GetPreloadRequests(_pieceViewDefinitionGetter))
            {
                _gameObjectPool.Preload(preloadRequest.Prefab, preloadRequest.Amount, preloadRequest.OnlyIfNeeded);
            }
        }

        [NotNull]
        protected abstract IEnumerable<PreloadRequest> GetPreloadRequests(
            IPieceViewDefinitionGetter pieceViewDefinitionGetter
        );
    }
}