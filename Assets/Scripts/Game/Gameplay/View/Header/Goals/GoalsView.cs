using System;
using System.Collections.Generic;
using Game.Common;
using Game.Gameplay.Board;
using Game.Gameplay.Goals;
using Game.Gameplay.Pieces;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.Header.Goals
{
    public class GoalsView : IGoalsView
    {
        [NotNull] private readonly IGoalsContainer _goalsContainer;

        private InitializedLabel _initializedLabel;

        private IGoals _goals;

        public event Action OnUpdated;

        public IEnumerable<PieceType> PieceTypes => _goals?.PieceTypes;

        public GoalsView([NotNull] IGoalsContainer goalsContainer)
        {
            ArgumentNullException.ThrowIfNull(goalsContainer);

            _goalsContainer = goalsContainer;
        }

        public void Initialize()
        {
            _initializedLabel.SetInitialized();

            IGoals goals = _goalsContainer.Goals;

            InvalidOperationException.ThrowIfNull(goals);

            _goals = goals.Clone();
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();

            _goals = null;
        }

        public IGoal Get(PieceType pieceType)
        {
            InvalidOperationException.ThrowIfNull(_goals);

            return _goals.Get(pieceType);
        }

        public void SetCurrentAmount(PieceType pieceType, int currentAmount)
        {
            IGoal goal = Get(pieceType);

            if (goal.CurrentAmount == currentAmount)
            {
                return;
            }

            goal.CurrentAmount = currentAmount;

            OnUpdated?.Invoke();
        }
    }
}