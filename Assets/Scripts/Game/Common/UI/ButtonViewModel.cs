using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Common.UI
{
    public class ButtonViewModel : ViewModel, IDataSettable<ButtonViewData>
    {
        [SerializeField] private Sprite _normalSprite;
        [SerializeField] private Sprite _pressedSprite;

        [NotNull] private readonly IBoundProperty<Sprite> _sprite = new BoundProperty<Sprite>("Sprite");

        private ButtonViewData _buttonViewData;
        private bool _pressed;

        protected override void Awake()
        {
            base.Awake();

            Add(_sprite);

            Add(new BoundMethod(OnPointerDown));
            Add(new BoundMethod(OnPointerUp));
        }

        public void SetData([NotNull] ButtonViewData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            _buttonViewData = data;

            RefreshSprite();
        }

        private void OnPointerDown()
        {
            _pressed = true;

            RefreshSprite();
        }

        private void OnPointerUp()
        {
            InvalidOperationException.ThrowIfNull(_buttonViewData);

            _buttonViewData.OnClick?.Invoke();

            _pressed = false;

            RefreshSprite();
        }

        private void RefreshSprite()
        {
            _sprite.Value = _pressed ? _pressedSprite : _normalSprite;
        }
    }
}