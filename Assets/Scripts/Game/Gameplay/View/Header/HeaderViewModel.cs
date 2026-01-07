using Game.Gameplay.View.EventResolvers;
using Game.Gameplay.View.Goals;
using Game.Gameplay.View.Moves;
using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel;
using Infrastructure.ModelViewViewModel.Examples.Button;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Header
{
    public class HeaderViewModel : ViewModel, IDataSettable<HeaderViewData>
    {
        private IEventsResolver _eventsResolver;

        [NotNull] private readonly IBoundProperty<ButtonViewData> _menuButtonViewData = new BoundProperty<ButtonViewData>("MenuButtonViewData");
        [NotNull] private readonly IBoundProperty<GoalsViewData> _goalsViewData = new BoundProperty<GoalsViewData>("GoalsViewData");
        [NotNull] private readonly IBoundProperty<MovesViewData> _movesViewData = new BoundProperty<MovesViewData>("MovesViewData");

        private void Awake()
        {
            InjectResolver.Resolve(this);

            InitializeBindings();
            AddBindings();
            SubscribeToEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        public void Inject([NotNull] IEventsResolver eventsResolver)
        {
            ArgumentNullException.ThrowIfNull(eventsResolver);

            _eventsResolver = eventsResolver;
        }

        public void SetData(HeaderViewData _)
        {
            // TODO: Remove if not needed (the same for HeaderViewData, etc)
        }

        private void InitializeBindings()
        {
            _menuButtonViewData.Value = new ButtonViewData(null, false); // TODO
            _goalsViewData.Value = new GoalsViewData();
            _movesViewData.Value = new MovesViewData();
        }

        private void AddBindings()
        {
            Add(_menuButtonViewData);
            Add(_goalsViewData);
            Add(_movesViewData);
        }

        private void SubscribeToEvents()
        {
            InvalidOperationException.ThrowIfNull(_eventsResolver);

            UnsubscribeFromEvents();

            _eventsResolver.OnResolveBegin += HandleResolveBegin;
            _eventsResolver.OnResolveEnd += HandleResolveEnd;
        }

        private void UnsubscribeFromEvents()
        {
            InvalidOperationException.ThrowIfNull(_eventsResolver);

            _eventsResolver.OnResolveBegin -= HandleResolveBegin;
            _eventsResolver.OnResolveEnd -= HandleResolveEnd;
        }

        private void HandleResolveBegin()
        {
            InvalidOperationException.ThrowIfNull(_menuButtonViewData.Value);

            _menuButtonViewData.Value.SetEnabled(false);
        }

        private void HandleResolveEnd()
        {
            InvalidOperationException.ThrowIfNull(_menuButtonViewData.Value);

            _menuButtonViewData.Value.SetEnabled(true);
        }
    }
}