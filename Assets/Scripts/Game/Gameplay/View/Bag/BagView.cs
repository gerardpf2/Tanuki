using System;
using Game.Common;
using Game.Common.Pieces;

namespace Game.Gameplay.View.Bag
{
    public class BagView : IBagView
    {
        private InitializedLabel _initializedLabel;

        private PieceType? _next;

        public event Action OnUpdated;

        public PieceType? Next
        {
            get => _next;
            set
            {
                if (Next == value)
                {
                    return;
                }

                _next = value;

                OnUpdated?.Invoke();
            }
        }

        public void Initialize()
        {
            _initializedLabel.SetInitialized();
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();

            Next = null;
        }
    }
}