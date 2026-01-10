using System;
using Game.Common.Pieces;

namespace Game.Gameplay.View.Bag
{
    public interface IBagView
    {
        event Action OnUpdated;

        PieceType? Next { get; set; }

        void Initialize();

        void Uninitialize();
    }
}