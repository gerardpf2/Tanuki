using System;
using Game.Gameplay.Events.Reasons;

namespace Game.Gameplay.View.Pieces.Pieces
{
    public interface IPieceViewEventNotifier
    {
        void OnInstantiated(InstantiatePieceReason instantiatePieceReason, Action onComplete);

        void OnDestroyed(DestroyPieceReason destroyPieceReason, Action onComplete);

        void OnRotated();
    }
}