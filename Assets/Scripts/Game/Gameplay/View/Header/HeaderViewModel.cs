using Game.Gameplay.View.Goals;
using Game.Gameplay.View.Moves;
using Infrastructure.ModelViewViewModel;
using Infrastructure.ModelViewViewModel.Examples.Button;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Header
{
    public class HeaderViewModel : ViewModel, IDataSettable<HeaderViewData>
    {
        [NotNull] private readonly IBoundProperty<ButtonViewData> _menuButtonViewData = new BoundProperty<ButtonViewData>("MenuButtonViewData");
        [NotNull] private readonly IBoundProperty<GoalsViewData> _goalsViewData = new BoundProperty<GoalsViewData>("GoalsViewData");
        [NotNull] private readonly IBoundProperty<MovesViewData> _movesViewData = new BoundProperty<MovesViewData>("MovesViewData");

        private void Awake()
        {
            InitializeBindings();
            AddBindings();
        }

        public void SetData(HeaderViewData _)
        {
            // TODO: Remove if not needed (the same for HeaderViewData, etc)
        }

        private void InitializeBindings()
        {
            _menuButtonViewData.Value = new ButtonViewData(null); // TODO
            _goalsViewData.Value = new GoalsViewData();
            _movesViewData.Value = new MovesViewData();
        }

        private void AddBindings()
        {
            Add(_menuButtonViewData);
            Add(_goalsViewData);
            Add(_movesViewData);
        }
    }
}