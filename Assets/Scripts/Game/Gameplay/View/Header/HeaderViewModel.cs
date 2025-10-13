using Game.Gameplay.View.Header.Goals;
using Game.Gameplay.View.Header.Moves;
using Infrastructure.ModelViewViewModel;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Header
{
    public class HeaderViewModel : ViewModel, IDataSettable<HeaderViewData>
    {
        [NotNull] private readonly IBoundProperty<GoalsViewData> _goalsViewData = new BoundProperty<GoalsViewData>("GoalsViewData");
        [NotNull] private readonly IBoundProperty<MovesViewData> _movesViewData = new BoundProperty<MovesViewData>("MovesViewData");

        protected override void Awake()
        {
            base.Awake();

            Add(_goalsViewData);
            Add(_movesViewData);
        }

        public void SetData(HeaderViewData _)
        {
            _goalsViewData.Value = new GoalsViewData();
            _movesViewData.Value = new MovesViewData();
        }
    }
}