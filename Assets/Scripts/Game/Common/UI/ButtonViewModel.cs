using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Common.UI
{
    public class ButtonViewModel : ViewModel, IDataSettable<ButtonViewData>
    {
        private ButtonViewData _buttonViewData;

        protected override void Awake()
        {
            base.Awake();

            Add(new BoundMethod(OnPointerDown));
            Add(new BoundMethod(OnPointerUp));
        }

        public void SetData([NotNull] ButtonViewData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            _buttonViewData = data;
        }

        private void OnPointerDown()
        {

        }

        private void OnPointerUp()
        {
            InvalidOperationException.ThrowIfNull(_buttonViewData);

            _buttonViewData.OnClick?.Invoke();
        }
    }
}