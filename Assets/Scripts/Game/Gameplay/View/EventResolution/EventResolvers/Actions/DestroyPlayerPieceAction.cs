using Game.Gameplay.EventEnqueueing.Events.Reasons;
using Game.Gameplay.View.Player;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.EventResolution.EventResolvers.Actions
{
    public class DestroyPlayerPieceAction : BaseDestroyPieceAction
    {
        [NotNull] private readonly IPiecePlayerView _piecePlayerView;

        public DestroyPlayerPieceAction(DestroyPieceReason destroyPieceReason, [NotNull] IPiecePlayerView piecePlayerView) : base(destroyPieceReason)
        {
            ArgumentNullException.ThrowIfNull(piecePlayerView);

            _piecePlayerView = piecePlayerView;
        }

        protected override GameObject GetPieceInstance()
        {
            GameObject instance = _piecePlayerView.Instance;

            InvalidOperationException.ThrowIfNull(instance);

            return instance;
        }

        protected override void DestroyPiece()
        {
            _piecePlayerView.Destroy();
        }
    }
}