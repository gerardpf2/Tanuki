using Game.Gameplay.View.Board;

namespace Game.Gameplay.View
{
    public class GameplayViewData
    {
        public readonly BoardViewData BoardViewData;

        public GameplayViewData(BoardViewData boardViewData)
        {
            BoardViewData = boardViewData;
        }
    }
}