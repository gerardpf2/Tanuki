using Infrastructure.ModelViewViewModel;

namespace Game.Gameplay.View.Board
{
    public class BoardViewModel : ViewModel, IDataSettable<BoardViewData>
    {
        public void SetData(BoardViewData data) { }
    }
}