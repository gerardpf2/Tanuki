using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Moves
{
    public class MovesViewModel : ViewModel, IDataSettable<MovesViewData>
    {
        private IMovesView _movesView;

        [NotNull] private readonly IBoundProperty<string> _amount = new BoundProperty<string>("Amount");

        protected override void Awake()
        {
            base.Awake();

            InjectResolver.Resolve(this);

            Add(_amount);
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        public void Inject([NotNull] IMovesView movesView)
        {
            ArgumentNullException.ThrowIfNull(movesView);

            _movesView = movesView;
        }

        public void SetData(MovesViewData _)
        {
            SubscribeToEvents();
            UpdateAmount();
        }

        private void SubscribeToEvents()
        {
            UnsubscribeFromEvents();

            InvalidOperationException.ThrowIfNull(_movesView);

            _movesView.OnUpdated += UpdateAmount;
        }

        private void UnsubscribeFromEvents()
        {
            InvalidOperationException.ThrowIfNull(_movesView);

            _movesView.OnUpdated -= UpdateAmount;
        }

        private void UpdateAmount()
        {
            InvalidOperationException.ThrowIfNull(_movesView);

            _amount.Value = _movesView.Amount.ToString();
        }
    }
}