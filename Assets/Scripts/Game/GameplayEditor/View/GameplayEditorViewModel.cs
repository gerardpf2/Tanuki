using Game.Gameplay.View.Board;
using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.GameplayEditor.View
{
    public class GameplayEditorViewModel : ViewModel, IDataSettable<GameplayEditorViewData>
    {
        [NotNull] private readonly IBoundProperty<BoardViewData> _boardViewData = new BoundProperty<BoardViewData>("BoardViewData");

        private void Awake()
        {
            Add(_boardViewData);
        }

        public void SetData([NotNull] GameplayEditorViewData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            _boardViewData.Value = new BoardViewData(data.OnReady);
        }
    }
}