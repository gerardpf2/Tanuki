using System;
using Game.Gameplay.EventEnqueueing.Events.Reasons;

namespace Game.Gameplay.View.Board.Pieces
{
    public interface IPieceViewEventNotifier
    {
        void OnInstantiated(InstantiateReason instantiateReason, Action onComplete);
    }
}