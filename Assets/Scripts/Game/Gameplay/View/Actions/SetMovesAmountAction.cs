using System;
using Game.Gameplay.View.Header.Moves;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.Actions
{
    public class SetMovesAmountAction : IAction
    {
        [NotNull] private readonly IMovesView _movesView;
        private readonly int _amount;

        public SetMovesAmountAction([NotNull] IMovesView movesView, int amount)
        {
            ArgumentNullException.ThrowIfNull(movesView);

            _movesView = movesView;
            _amount = amount;
        }

        public void Resolve(Action onComplete)
        {
            _movesView.Amount = _amount;

            onComplete?.Invoke();
        }
    }
}