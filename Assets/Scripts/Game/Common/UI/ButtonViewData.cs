using System;

namespace Game.Common.UI
{
    public class ButtonViewData
    {
        public readonly Action OnClick;

        public ButtonViewData(Action onClick)
        {
            OnClick = onClick;
        }
    }
}