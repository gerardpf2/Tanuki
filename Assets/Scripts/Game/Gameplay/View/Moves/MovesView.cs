using System;
using Game.Common;
using Game.Gameplay.Moves;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.Moves
{
    public class MovesView : IMovesView
    {
        [NotNull] private readonly IMoves _modelMoves;
        [NotNull] private readonly IMoves _viewMoves;

        private InitializedLabel _initializedLabel;

        public event Action OnUpdated;

        public int Amount
        {
            get => _viewMoves.Amount;
            set
            {
                if (Amount == value)
                {
                    return;
                }

                _viewMoves.Amount = value;

                OnUpdated?.Invoke();
            }
        }

        public MovesView([NotNull] IMoves modelMoves, [NotNull] IMoves viewMoves)
        {
            ArgumentNullException.ThrowIfNull(modelMoves);
            ArgumentNullException.ThrowIfNull(viewMoves);

            _modelMoves = modelMoves;
            _viewMoves = viewMoves;
        }

        public void Initialize()
        {
            _initializedLabel.SetInitialized();

            _viewMoves.Amount = _modelMoves.Amount;
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();

            _viewMoves.Reset();
        }
    }
}