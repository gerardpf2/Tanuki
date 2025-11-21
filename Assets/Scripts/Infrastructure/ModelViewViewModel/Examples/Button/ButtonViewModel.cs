using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.ModelViewViewModel.Examples.Button
{
    public class ButtonViewModel : ViewModel, IDataSettable<ButtonViewData>
    {
        [SerializeField] private Sprite _normalSprite;
        [SerializeField] private Sprite _pressedSprite;
        [SerializeField] private Sprite _disabledSprite;

        [NotNull] private readonly IBoundProperty<Sprite> _sprite = new BoundProperty<Sprite>("Sprite");

        private ButtonViewData _buttonViewData;
        private bool _pressed;

        private void Awake()
        {
            Add(_sprite);

            Add(new BoundMethod("OnPointerDown", HandlePointerDown));
            Add(new BoundMethod("OnPointerUp", HandlePointerUp));
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        public void SetData([NotNull] ButtonViewData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            UnsubscribeFromEvents();

            _buttonViewData = data;

            SubscribeToEvents();
            RefreshSprite();
        }

        private void SubscribeToEvents()
        {
            InvalidOperationException.ThrowIfNull(_buttonViewData);

            UnsubscribeFromEvents();

            _buttonViewData.OnEnabledUpdated += HandleEnabledUpdated;
        }

        private void UnsubscribeFromEvents()
        {
            if (_buttonViewData is not null)
            {
                _buttonViewData.OnEnabledUpdated -= HandleEnabledUpdated;
            }
        }

        private void HandlePointerDown()
        {
            _pressed = true;

            RefreshSprite();
        }

        private void HandlePointerUp()
        {
            InvalidOperationException.ThrowIfNull(_buttonViewData);

            if (_buttonViewData.Enabled)
            {
                _buttonViewData.OnClick?.Invoke();
            }

            _pressed = false;

            RefreshSprite();
        }

        private void HandleEnabledUpdated()
        {
            RefreshSprite();
        }

        private void RefreshSprite()
        {
            InvalidOperationException.ThrowIfNull(_buttonViewData);

            if (!_buttonViewData.Enabled)
            {
                _sprite.Value = _disabledSprite;
            }
            else
            {
                _sprite.Value = _pressed ? _pressedSprite : _normalSprite;
            }
        }
    }
}