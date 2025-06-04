using Game.Gameplay.View.Header.Goals;

namespace Game.Gameplay.View.Header
{
    public class HeaderViewData
    {
        public readonly GoalsViewData GoalsViewData;

        public HeaderViewData(GoalsViewData goalsViewData)
        {
            GoalsViewData = goalsViewData;
        }
    }
}