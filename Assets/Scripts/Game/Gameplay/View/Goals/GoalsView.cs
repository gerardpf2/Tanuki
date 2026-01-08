using System;
using System.Collections.Generic;
using System.Linq;
using Game.Common;
using Game.Common.Pieces;
using Game.Gameplay.Goals;
using Game.Gameplay.Goals.Utils;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.Goals
{
    public class GoalsView : IGoalsView
    {
        [NotNull] private readonly IGoals _modelGoals;
        [NotNull] private readonly IGoals _viewGoals;

        private InitializedLabel _initializedLabel;

        public event Action OnUpdated;

        public IEnumerable<IGoal> Entries => _viewGoals.Entries;

        public GoalsView([NotNull] IGoals modelGoals, [NotNull] IGoals viewGoals)
        {
            ArgumentNullException.ThrowIfNull(modelGoals);
            ArgumentNullException.ThrowIfNull(viewGoals);

            _modelGoals = modelGoals;
            _viewGoals = viewGoals;
        }

        public void Initialize()
        {
            _initializedLabel.SetInitialized();

            _viewGoals.Add(_modelGoals.Entries.Select(goal => goal.Clone()));

            OnUpdated?.Invoke();
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();

            _viewGoals.Clear();
        }

        public void SetCurrentAmount(PieceType pieceType, int currentAmount)
        {
            IGoal goal = _viewGoals.Get(pieceType);

            if (goal.CurrentAmount == currentAmount)
            {
                return;
            }

            goal.CurrentAmount = currentAmount;

            OnUpdated?.Invoke();
        }
    }
}