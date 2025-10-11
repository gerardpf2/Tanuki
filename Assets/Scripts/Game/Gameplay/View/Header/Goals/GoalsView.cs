using System;
using Game.Common;
using Game.Gameplay.Board;
using Game.Gameplay.Goals;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.Header.Goals
{
    public class GoalsView : IGoalsView
    {
        [NotNull] private readonly IGoalsContainer _goalsContainer;

        private InitializedLabel _initializedLabel;

        public event Action OnUpdated;

        public IGoals Goals { get; private set; }

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

            Goals = goals.Clone();
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();

            Goals = null;
        }

        public void SetCurrentAmount(PieceType pieceType, int currentAmount)
        {
            InvalidOperationException.ThrowIfNull(Goals);

            IGoal goal = Goals.Get(pieceType);

            if (goal.CurrentAmount == currentAmount)
            {
                return;
            }

            goal.CurrentAmount = currentAmount;

            OnUpdated?.Invoke();
        }
    }
}