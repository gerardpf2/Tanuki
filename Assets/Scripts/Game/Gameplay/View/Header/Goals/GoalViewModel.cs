using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Header.Goals
{
    public class GoalViewModel : ViewModel, IDataSettable<GoalViewData>
    {
        // TODO: PieceType

        [NotNull] private readonly IBoundProperty<int> _initialAmount = new BoundProperty<int>("InitialAmount", 0);
        [NotNull] private readonly IBoundProperty<int> _currentAmount = new BoundProperty<int>("CurrentAmount", 0);

        protected override void Awake()
        {
            base.Awake();

            Add(_initialAmount);
            Add(_currentAmount);
        }

        public void SetData([NotNull] GoalViewData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            _initialAmount.Value = data.InitialAmount;
            _currentAmount.Value = data.CurrentAmount;
        }
    }
}