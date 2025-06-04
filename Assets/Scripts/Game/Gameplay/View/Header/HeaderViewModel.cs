using Game.Gameplay.View.Header.Goals;
using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
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

        public void SetData([NotNull] HeaderViewData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            _goalsViewData.Value = data.GoalsViewData;
        }
    }
}