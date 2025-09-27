using System;
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

            Goals = goals.Clone();
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