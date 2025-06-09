using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Header.Goals
{
    public class GoalViewModel : ViewModel, IDataSettable<GoalViewData>
    {
        // TODO: PieceType

        [NotNull] private readonly IBoundProperty<string> _initialAmount = new BoundProperty<string>("InitialAmount");
        [NotNull] private readonly IBoundProperty<string> _currentAmount = new BoundProperty<string>("CurrentAmount");

        protected override void Awake()
        {
            base.Awake();

            Add(_initialAmount);
            Add(_currentAmount);
        }

        public void SetData([NotNull] GoalViewData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            _initialAmount.Value = data.InitialAmount.ToString();
            _currentAmount.Value = data.CurrentAmount.ToString();
        }
    }
}