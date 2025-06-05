using Game.Gameplay.View.Header.Goals;
using Infrastructure.ModelViewViewModel;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Header
{
    public class HeaderViewModel : ViewModel, IDataSettable<HeaderViewData>
    {
        [NotNull] private readonly IBoundProperty<GoalsViewData> _goalsViewData = new BoundProperty<GoalsViewData>("GoalsViewData", null);

        protected override void Awake()
        {
            base.Awake();

            Add(_goalsViewData);
        }

        public void SetData(HeaderViewData _)
        {
            _goalsViewData.Value = new GoalsViewData();
        }
    }
}