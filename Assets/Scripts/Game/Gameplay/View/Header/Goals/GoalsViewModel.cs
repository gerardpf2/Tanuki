using System.Collections.Generic;
using Game.Gameplay.Board;
using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Header.Goals
{
    public class GoalsViewModel : ViewModel, IDataSettable<GoalsViewData>
    {
        private IGoalsViewContainer _goalsViewContainer;

        [NotNull] private readonly IBoundProperty<IEnumerable<GoalViewData>> _goalsViewData = new BoundProperty<IEnumerable<GoalViewData>>("GoalsViewData", null);

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

        public void Inject([NotNull] IGoalsViewContainer goalsViewContainer)
        {
            ArgumentNullException.ThrowIfNull(goalsViewContainer);

            _goalsViewContainer = goalsViewContainer;
        }

        public void SetData(GoalsViewData _)
        {
            SubscribeToEvents();
            UpdateGoals();
        }

        private void SubscribeToEvents()
        {
            UnsubscribeFromEvents();

            InvalidOperationException.ThrowIfNull(_goalsViewContainer);

            _goalsViewContainer.OnUpdated += UpdateGoals;
        }

        private void UnsubscribeFromEvents()
        {
            InvalidOperationException.ThrowIfNull(_goalsViewContainer);

            _goalsViewContainer.OnUpdated -= UpdateGoals;
        }

        private void UpdateGoals()
        {
            InvalidOperationException.ThrowIfNull(_goalsViewContainer);

            ICollection<GoalViewData> goalsViewData = new List<GoalViewData>();

            foreach (PieceType pieceType in _goalsViewContainer.PieceTypes)
            {
                GoalViewData goalViewData = new(
                    pieceType,
                    _goalsViewContainer.GetInitialAmount(pieceType),
                    _goalsViewContainer.GetCurrentAmount(pieceType)
                );

                goalsViewData.Add(goalViewData);
            }

            _goalsViewData.Value = goalsViewData;
        }
    }
}