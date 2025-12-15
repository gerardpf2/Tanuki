using Game.Gameplay.View.Board;
using Game.Gameplay.View.Header;
using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View
{
    public class GameplayViewModel : ViewModel, IDataSettable<GameplayViewData>
    {
        [NotNull] private readonly IBoundProperty<HeaderViewData> _headerViewData = new BoundProperty<HeaderViewData>("HeaderViewData");
        [NotNull] private readonly IBoundProperty<BoardViewData> _boardViewData = new BoundProperty<BoardViewData>("BoardViewData");

        private void Awake()
        {
            Add(_headerViewData);
            Add(_boardViewData);
        }

        public void SetData([NotNull] GameplayViewData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            _headerViewData.Value = new HeaderViewData();
            _boardViewData.Value = new BoardViewData(data.OnReady);
        }
    }
}