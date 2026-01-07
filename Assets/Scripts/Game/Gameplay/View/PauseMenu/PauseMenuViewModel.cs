using Game.Gameplay.View.PauseMenu.UseCases;
using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel;
using Infrastructure.ModelViewViewModel.Examples.Button;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.PauseMenu
{
    public class PauseMenuViewModel : ViewModel
    {
        private IResumeGameplayUseCase _resumeGameplayUseCase;
        private IGoToMainMenuUseCase _goToMainMenuUseCase;

        [NotNull] private readonly IBoundProperty<ButtonViewData> _resumeButtonViewData = new BoundProperty<ButtonViewData>("ResumeButtonViewData");
        [NotNull] private readonly IBoundProperty<ButtonViewData> _restartButtonViewData = new BoundProperty<ButtonViewData>("RestartButtonViewData");
        [NotNull] private readonly IBoundProperty<ButtonViewData> _mainMenuButtonViewData = new BoundProperty<ButtonViewData>("MainMenuButtonViewData");

        private void Awake()
        {
            InjectResolver.Resolve(this);

            InitializeBindings();
            AddBindings();
        }

        public void Inject(
            [NotNull] IResumeGameplayUseCase resumeGameplayUseCase,
            [NotNull] IGoToMainMenuUseCase goToMainMenuUseCase)
        {
            ArgumentNullException.ThrowIfNull(resumeGameplayUseCase);
            ArgumentNullException.ThrowIfNull(resumeGameplayUseCase);

            _resumeGameplayUseCase = resumeGameplayUseCase;
            _goToMainMenuUseCase = goToMainMenuUseCase;
        }

        private void InitializeBindings()
        {
            _resumeButtonViewData.Value = new ButtonViewData(HandleResumeButtonClick);
            _restartButtonViewData.Value = new ButtonViewData(HandleRestartButtonClick);
            _mainMenuButtonViewData.Value = new ButtonViewData(HandleMainMenuButtonClick);
        }

        private void AddBindings()
        {
            Add(_resumeButtonViewData);
            Add(_restartButtonViewData);
            Add(_mainMenuButtonViewData);
        }

        private void HandleResumeButtonClick()
        {
            InvalidOperationException.ThrowIfNull(_resumeGameplayUseCase);

            _resumeGameplayUseCase.Resolve();
        }

        private static void HandleRestartButtonClick()
        {
            // TODO
        }

        private void HandleMainMenuButtonClick()
        {
            InvalidOperationException.ThrowIfNull(_goToMainMenuUseCase);

            _goToMainMenuUseCase.Resolve();
        }
    }
}