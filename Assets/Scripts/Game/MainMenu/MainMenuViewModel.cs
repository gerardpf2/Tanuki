using Game.Gameplay.View.UseCases;
using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel;
using Infrastructure.ModelViewViewModel.Examples.Button;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.MainMenu
{
    public class MainMenuViewModel : ViewModel
    {
        private ILoadGameplayUseCase _loadGameplayUseCase;

        [NotNull] private readonly IBoundProperty<ButtonViewData> _loadGameplay = new BoundProperty<ButtonViewData>("LoadGameplayButtonViewData");

        private void Awake()
        {
            InjectResolver.Resolve(this);

            InitializeBindings();
            AddBindings();
        }

        public void Inject([NotNull] ILoadGameplayUseCase loadGameplayUseCase)
        {
            ArgumentNullException.ThrowIfNull(loadGameplayUseCase);

            _loadGameplayUseCase = loadGameplayUseCase;
        }

        private void InitializeBindings()
        {
            _loadGameplay.Value = new ButtonViewData(HandleLoadGameplayClick);
        }

        private void AddBindings()
        {
            Add(_loadGameplay);
        }

        private void HandleLoadGameplayClick()
        {
            InvalidOperationException.ThrowIfNull(_loadGameplayUseCase);

            _loadGameplayUseCase.Resolve("Test");
        }
    }
}