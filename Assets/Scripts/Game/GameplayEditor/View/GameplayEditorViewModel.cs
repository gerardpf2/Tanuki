using Game.Gameplay.View.Board;
using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.GameplayEditor.View
{
    public class GameplayEditorViewModel : ViewModel, IDataSettable<GameplayEditorViewData>
    {
        [NotNull] private readonly IBoundProperty<BoardViewData> _boardEditorViewData = new BoundProperty<BoardViewData>("BoardEditorViewData");

        private void Awake()
        {
            Add(_boardEditorViewData);
        }

        public void SetData([NotNull] GameplayEditorViewData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            _boardEditorViewData.Value = new BoardViewData(data.OnReady);
        }
    }
}