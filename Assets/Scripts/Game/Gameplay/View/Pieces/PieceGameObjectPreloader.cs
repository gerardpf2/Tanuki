using System;
using Game.Common.Pieces;
using Infrastructure.Unity.Pooling;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.Pieces
{
    public class PieceGameObjectPreloader : IPieceGameObjectPreloader
    {
        [NotNull] private readonly IPieceViewDefinitionGetter _pieceViewDefinitionGetter;
        [NotNull] private readonly IGameObjectPool _gameObjectPool;

        public PieceGameObjectPreloader(
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
            // TODO: Use board container piece placements and bag initial piece types and piece entries

            const int amount = 5;

            foreach (object pieceTypeObj in Enum.GetValues(typeof(PieceType)))
            {
                PieceType pieceType = (PieceType)pieceTypeObj;
                IPieceViewDefinition pieceViewDefinition = _pieceViewDefinitionGetter.Get(pieceType);

                _gameObjectPool.Preload(pieceViewDefinition.Prefab, amount);
            }
        }
    }
}