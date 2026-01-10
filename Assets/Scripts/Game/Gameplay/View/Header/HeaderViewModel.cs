using Game.Gameplay.View.EventResolvers;
using Game.Gameplay.View.Goals;
using Game.Gameplay.View.Moves;
using Game.Gameplay.View.PauseMenu.UseCases;
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
        private ILoadPauseMenuUseCase _loadPauseMenuUseCase;

        [NotNull] private readonly IBoundProperty<ButtonViewData> _pauseMenuButtonViewData = new BoundProperty<ButtonViewData>("PauseMenuButtonViewData");
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

        public void Inject(
            [NotNull] IEventsResolver eventsResolver,
            [NotNull] ILoadPauseMenuUseCase loadPauseMenuUseCase)
        {
            ArgumentNullException.ThrowIfNull(eventsResolver);
            ArgumentNullException.ThrowIfNull(loadPauseMenuUseCase);

            _eventsResolver = eventsResolver;
            _loadPauseMenuUseCase = loadPauseMenuUseCase;
        }

        public void SetData(HeaderViewData _)
        {
            // TODO: Remove if not needed (the same for HeaderViewData, etc)
        }

        private void InitializeBindings()
        {
            _pauseMenuButtonViewData.Value = new ButtonViewData(HandlePauseMenuButtonClick, enabled: false);
            _goalsViewData.Value = new GoalsViewData();
            _movesViewData.Value = new MovesViewData();
        }

        private void AddBindings()
        {
            Add(_pauseMenuButtonViewData);
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
            InvalidOperationException.ThrowIfNull(_pauseMenuButtonViewData.Value);

            _pauseMenuButtonViewData.Value.SetEnabled(false);
        }

        private void HandleResolveEnd()
        {
            InvalidOperationException.ThrowIfNull(_pauseMenuButtonViewData.Value);

            _pauseMenuButtonViewData.Value.SetEnabled(true);
        }

        private void HandlePauseMenuButtonClick()
        {
            InvalidOperationException.ThrowIfNull(_loadPauseMenuUseCase);

            _loadPauseMenuUseCase.Resolve();
        }
    }
}