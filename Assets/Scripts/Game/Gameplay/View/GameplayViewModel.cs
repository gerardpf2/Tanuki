using Infrastructure.ModelViewViewModel;

namespace Game.Gameplay.View
{
    public class GameplayViewModel : ViewModel, IDataSettable<GameplayViewData>
    {
        public void SetData(GameplayViewData data) { }
    }
}