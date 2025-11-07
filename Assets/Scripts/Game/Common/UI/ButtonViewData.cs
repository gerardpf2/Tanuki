using System;

namespace Game.Common.UI
{
    public class ButtonViewData
    {
        public event Action OnEnabledUpdated;

        public readonly Action OnClick;

        public bool Enabled { get; private set; }

        public ButtonViewData(Action onClick, bool enabled = true)
        {
            OnClick = onClick;
            Enabled = enabled;
        }

        public void SetEnabled(bool enabled)
        {
            if (Enabled == enabled)
            {
                return;
            }

            Enabled = enabled;

            OnEnabledUpdated?.Invoke();
        }
    }
}