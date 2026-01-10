using System;

namespace Infrastructure.ModelViewViewModel.Examples.Button
{
    public class ButtonViewData
    {
        public event Action OnEnabledUpdated;

        public readonly Action OnClick;
        public readonly Action OnClickDisabled;

        public bool Enabled { get; private set; }

        public ButtonViewData(Action onClick, Action onClickDisabled = null, bool enabled = true)
        {
            OnClick = onClick;
            OnClickDisabled = onClickDisabled;

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