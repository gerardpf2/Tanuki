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

            Add(new BoundMethod(OnClick));
        }

        public void SetData([NotNull] ButtonViewData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            _buttonViewData = data;
        }

        private void OnClick()
        {
            InvalidOperationException.ThrowIfNull(_buttonViewData);

            _buttonViewData.OnClick?.Invoke();
        }
    }
}