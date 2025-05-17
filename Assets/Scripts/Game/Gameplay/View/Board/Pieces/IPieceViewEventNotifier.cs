using System;
using Game.Gameplay.EventEnqueueing.Events.Reasons;

namespace Game.Gameplay.View.Board.Pieces
{
    public interface IPieceViewEventNotifier
    {
        void OnInstantiated(InstantiatePieceReason instantiatePieceReason, Action onComplete);

        void OnDestroyed(Action onComplete); // TODO: Add reason
    }
}