using Infrastructure.System.Exceptions;
using Infrastructure.UnityUtils;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.ModelViewViewModel.Examples.Button
{
    public class ButtonViewModel : ViewModel, IDataSettable<ButtonViewData>
    {
        private static readonly Vector3 DefaultScale = Vector3.one;

        [SerializeField] private bool _scaleOnClick = true;
        [SerializeField, ShowInInspectorIf(nameof(_scaleOnClick))] private float _scaleXOnClick = 1.1f;
        [SerializeField, ShowInInspectorIf(nameof(_scaleOnClick))] private float _scaleYOnClick = 0.9f;

        [NotNull] private readonly IBoundProperty<bool> _enabled = new BoundProperty<bool>("Enabled");
        [NotNull] private readonly IBoundProperty<Vector3> _scale = new BoundProperty<Vector3>("Scale", DefaultScale);

        private ButtonViewData _buttonViewData;

        protected virtual void Awake()
        {
            Add(_enabled);
            Add(_scale);

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

        private void HandlePointerDown()
        {
            if (!_scaleOnClick)
            {
                return;
            }

            _scale.Value = new Vector3(_scaleXOnClick, _scaleYOnClick, DefaultScale.z);
        }

        private void HandlePointerUp()
        {
            _scale.Value = DefaultScale;
        }

        private void HandleClick()
        {
            InvalidOperationException.ThrowIfNull(_buttonViewData);

            if (_buttonViewData.Enabled)
            {
                _buttonViewData.OnClick?.Invoke();
            }
            else
            {
                _buttonViewData.OnClickDisabled?.Invoke();
            }
        }
    }
}