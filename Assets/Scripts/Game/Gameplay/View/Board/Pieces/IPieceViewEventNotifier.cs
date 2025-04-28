using System;

namespace Game.Gameplay.View.Board.Pieces
{
    public interface IPieceViewEventNotifier
    {
        void OnInstantiated(Action onComplete); // TODO: InstantiateReason
    }
}