using Game.Common.Pieces;
using Game.Gameplay.Bag;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity.Pooling;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Pieces.Preloader
{
    public class PlayerPieceGhostsGameObjectPreloader : BasePlayerPiecesGameObjectPreloader
    {
        public PlayerPieceGhostsGameObjectPreloader(
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IGameObjectPool gameObjectPool,
            [NotNull] IBag bag) : base(pieceViewDefinitionGetter, gameObjectPool, bag) { }

        protected override IPieceViewDefinition GetPieceViewDefinition(
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            PieceType pieceType)
        {
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);

            return pieceViewDefinitionGetter.GetPlayerPieceGhost(pieceType);
        }
    }
}