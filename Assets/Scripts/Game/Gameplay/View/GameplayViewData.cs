using Game.Gameplay.View.Board;
using Game.Gameplay.View.Header;

namespace Game.Gameplay.View
{
    public class GameplayViewData
    {
        public readonly HeaderViewData HeaderViewData;
        public readonly BoardViewData BoardViewData;

        public GameplayViewData(HeaderViewData headerViewData, BoardViewData boardViewData)
        {
            HeaderViewData = headerViewData;
            BoardViewData = boardViewData;
        }
    }
}