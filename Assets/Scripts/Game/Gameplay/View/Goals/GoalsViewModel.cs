using System.Collections.Generic;
using Game.Common.Pieces;
using Game.Gameplay.Goals;
using Game.Gameplay.Pieces;
using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Goals
{
    public class GoalsViewModel : ViewModel, IDataSettable<GoalsViewData>
    {
        private IGoalsView _goalsView;

        [NotNull] private readonly IBoundProperty<IEnumerable<GoalViewData>> _goalsViewData = new BoundProperty<IEnumerable<GoalViewData>>("GoalsViewData");

        protected override void Awake()
        {
            base.Awake();

            InjectResolver.Resolve(this);

            Add(_goalsViewData);
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        public void Inject([NotNull] IGoalsView goalsView)
        {
            ArgumentNullException.ThrowIfNull(goalsView);

            _goalsView = goalsView;
        }

        public void SetData(GoalsViewData _)
        {
            SubscribeToEvents();
            UpdateGoals();
        }

        private void SubscribeToEvents()
        {
            UnsubscribeFromEvents();

            InvalidOperationException.ThrowIfNull(_goalsView);

            _goalsView.OnUpdated += UpdateGoals;
        }

        private void UnsubscribeFromEvents()
        {
            InvalidOperationException.ThrowIfNull(_goalsView);

            _goalsView.OnUpdated -= UpdateGoals;
        }

        private void UpdateGoals()
        {
            InvalidOperationException.ThrowIfNull(_goalsView);
            InvalidOperationException.ThrowIfNull(_goalsView.PieceTypes);

            ICollection<GoalViewData> goalsViewData = new List<GoalViewData>();

            foreach (PieceType pieceType in _goalsView.PieceTypes)
            {
                IGoal goal = _goalsView.Get(pieceType);

                GoalViewData goalViewData = new(goal.PieceType, goal.InitialAmount, goal.CurrentAmount);

                goalsViewData.Add(goalViewData);
            }

            _goalsViewData.Value = goalsViewData;
        }
    }
}