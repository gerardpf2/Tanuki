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

        private IGoals _goals;

        public event Action OnUpdated;

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

            _goals = new Gameplay.Goals.Goals(goals.Targets.Select(goal => goal.Clone()));
        }

        public void Uninitialize()
        {
            _goals = null;
        }

        public void TryIncreaseCurrentAmount(PieceType pieceType)
        {
            InvalidOperationException.ThrowIfNull(_goals);

            if (_goals.TryIncreaseCurrentAmount(pieceType))
            {
                OnUpdated?.Invoke();
            }
        }
    }
}