using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Infrastructure.ModelViewViewModel.Examples.Button
{
    public class ButtonViewModel : ViewModel, IDataSettable<ButtonViewData>
    {
        [NotNull] private readonly IBoundProperty<bool> _enabled = new BoundProperty<bool>("Enabled");

        private ButtonViewData _buttonViewData;

        protected virtual void Awake()
        {
            Add(_enabled);

            Add(new BoundMethod("OnPointerDown", HandlePointerDown));
            Add(new BoundMethod("OnPointerUp", HandlePointerUp));
            Add(new BoundMethod("OnClick", HandleClick));
        }

        protected virtual void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        public void SetData([NotNull] ButtonViewData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            UnsubscribeFromEvents();

            _buttonViewData = data;

            SubscribeToEvents();
            RefreshEnabled();
        }

        private void SubscribeToEvents()
        {
            InvalidOperationException.ThrowIfNull(_buttonViewData);

            UnsubscribeFromEvents();

            _buttonViewData.OnEnabledUpdated += RefreshEnabled;
        }

        private void UnsubscribeFromEvents()
        {
            if (_buttonViewData is not null)
            {
                _buttonViewData.OnEnabledUpdated -= RefreshEnabled;
            }
        }

        private void RefreshEnabled()
        {
            InvalidOperationException.ThrowIfNull(_buttonViewData);

            _enabled.Value = _buttonViewData.Enabled;
        }

        private static void HandlePointerDown()
        {
            // TODO
        }

        private static void HandlePointerUp()
        {
            // TODO
        }

        private void HandleClick()
        {
            InvalidOperationException.ThrowIfNull(_buttonViewData);

            _buttonViewData.OnClick?.Invoke();
        }
    }
}