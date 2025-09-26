using System;
using System.Linq;
using Game.Gameplay.Board;
using Game.Gameplay.Goals;
using Game.Gameplay.Goals.Utils;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.Header.Goals
{
    public class GoalsView : IGoalsView
    {
        [NotNull] private readonly IGoalsContainer _goalsContainer;

        public event Action OnUpdated;

        public IGoals Goals { get; private set; }

        public GoalsView([NotNull] IGoalsContainer goalsContainer)
        {
            ArgumentNullException.ThrowIfNull(goalsContainer);

            _goalsContainer = goalsContainer;
        }

        public void Initialize()
        {
            IGoals goals = _goalsContainer.Goals;

            InvalidOperationException.ThrowIfNull(goals);

            Uninitialize();

            Goals = new Gameplay.Goals.Goals(goals.Targets.Select(Clone));

            return;

            [NotNull]
            IGoal Clone([NotNull] IGoal goal)
            {
                ArgumentNullException.ThrowIfNull(goal);

                return new Goal(goal.PieceType, goal.InitialAmount);
            }
        }

        public void Uninitialize()
        {
            Goals = null;
        }

        public void TryIncreaseCurrentAmount(PieceType pieceType)
        {
            InvalidOperationException.ThrowIfNull(Goals);

            if (Goals.TryIncreaseCurrentAmount(pieceType))
            {
                OnUpdated?.Invoke();
            }
        }
    }
}