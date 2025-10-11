using System;
using Game.Gameplay.Board;
using Game.Gameplay.View.Header.Goals;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.EventResolution.EventResolvers.Actions
{
    public class SetGoalCurrentAmountAction : IAction
    {
        [NotNull] private readonly IGoalsView _goalsView;
        private readonly PieceType _pieceType;
        private readonly int _currentAmount;

        public SetGoalCurrentAmountAction([NotNull] IGoalsView goalsView, PieceType pieceType, int currentAmount)
        {
            ArgumentNullException.ThrowIfNull(goalsView);

            _goalsView = goalsView;
            _pieceType = pieceType;
            _currentAmount = currentAmount;
        }

        public void Resolve(Action onComplete)
        {
            // TODO

            onComplete?.Invoke();
        }
    }
}