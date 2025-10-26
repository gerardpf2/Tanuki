using System;
using Game.Common;
using Game.Gameplay.Moves;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.Moves
{
    public class MovesView : IMovesView
    {
        [NotNull] private readonly IMovesContainer _movesContainer;

        private InitializedLabel _initializedLabel;

        private IMoves _moves;

        public event Action OnUpdated;

        public int Amount
        {
            get
            {
                InvalidOperationException.ThrowIfNull(_moves);

                return _moves.Amount;
            }
            set
            {
                InvalidOperationException.ThrowIfNull(_moves);

                if (Amount == value)
                {
                    return;
                }

                _moves.Amount = value;

                OnUpdated?.Invoke();
            }
        }

        public MovesView([NotNull] IMovesContainer movesContainer)
        {
            ArgumentNullException.ThrowIfNull(movesContainer);

            _movesContainer = movesContainer;
        }

        public void Initialize()
        {
            _initializedLabel.SetInitialized();

            IMoves moves = _movesContainer.Moves;

            InvalidOperationException.ThrowIfNull(moves);

            _moves = moves.Clone();
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();

            _moves = null;
        }
    }
}