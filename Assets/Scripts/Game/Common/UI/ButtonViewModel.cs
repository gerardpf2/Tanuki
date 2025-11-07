using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Common.UI
{
    public class ButtonViewModel : ViewModel, IDataSettable<ButtonViewData>
    {
        [SerializeField] private Sprite _normalBackground;
        [SerializeField] private Sprite _pressedBackground;
        [SerializeField] private Sprite _disabledBackground;

        [SerializeField] private Sprite _normalSprite;
        [SerializeField] private Sprite _pressedSprite;
        [SerializeField] private Sprite _disabledSprite;

        [NotNull] private readonly IBoundProperty<Sprite> _background = new BoundProperty<Sprite>("Background");
        [NotNull] private readonly IBoundProperty<Sprite> _sprite = new BoundProperty<Sprite>("Sprite");

        private ButtonViewData _buttonViewData;
        private bool _pressed;

        protected override void Awake()
        {
            base.Awake();

            Add(_background);
            Add(_sprite);

            Add(new BoundMethod(OnPointerDown));
            Add(new BoundMethod(OnPointerUp));
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
            RefreshImages();
        }

        private void SubscribeToEvents()
        {
            InvalidOperationException.ThrowIfNull(_buttonViewData);

            UnsubscribeFromEvents();

            _buttonViewData.OnEnabledUpdated += OnEnabledUpdated;
        }

        private void UnsubscribeFromEvents()
        {
            if (_buttonViewData is not null)
            {
                _buttonViewData.OnEnabledUpdated -= OnEnabledUpdated;
            }
        }

        private void OnPointerDown()
        {
            _pressed = true;

            RefreshImages();
        }

        private void OnPointerUp()
        {
            InvalidOperationException.ThrowIfNull(_buttonViewData);

            if (_buttonViewData.Enabled)
            {
                _buttonViewData.OnClick?.Invoke();
            }

            _pressed = false;

            RefreshImages();
        }

        private void OnEnabledUpdated()
        {
            RefreshImages();
        }

        private void RefreshImages()
        {
            Refresh(_background, _normalBackground, _pressedBackground, _disabledBackground);
            Refresh(_sprite, _normalSprite, _pressedSprite, _disabledSprite);

            return;

            void Refresh([NotNull] IBoundProperty<Sprite> boundProperty, Sprite normal, Sprite pressed, Sprite disabled)
            {
                ArgumentNullException.ThrowIfNull(boundProperty);
                InvalidOperationException.ThrowIfNull(_buttonViewData);

                if (!_buttonViewData.Enabled)
                {
                    boundProperty.Value = disabled;
                }
                else
                {
                    boundProperty.Value = _pressed ? pressed : normal;
                }
            }
        }
    }
}