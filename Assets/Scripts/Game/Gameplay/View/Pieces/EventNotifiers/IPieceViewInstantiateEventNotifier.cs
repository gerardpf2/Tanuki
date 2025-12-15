using System;
using Game.Gameplay.Events.Reasons;

namespace Game.Gameplay.View.Pieces.EventNotifiers
{
    public interface IPieceViewInstantiateEventNotifier
    {
        void OnInstantiated(InstantiatePieceReason instantiatePieceReason, Action onComplete);

        void OnDestroyed(DestroyPieceReason destroyPieceReason, Action onComplete);
    }
}