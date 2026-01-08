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
        private IGoToMainMenuUseCase _goToMainMenuUseCase;
        private IRestartGameplayUseCase _restartGameplayUseCase;
        private IResumeGameplayUseCase _resumeGameplayUseCase;

        [NotNull] private readonly IBoundProperty<ButtonViewData> _goToMainMenuButtonViewData = new BoundProperty<ButtonViewData>("GoToMainMenuButtonViewData");
        [NotNull] private readonly IBoundProperty<ButtonViewData> _restartGameplayButtonViewData = new BoundProperty<ButtonViewData>("RestartGameplayButtonViewData");
        [NotNull] private readonly IBoundProperty<ButtonViewData> _resumeGameplayButtonViewData = new BoundProperty<ButtonViewData>("ResumeGameplayButtonViewData");

        private void Awake()
        {
            InjectResolver.Resolve(this);

            InitializeBindings();
            AddBindings();
        }

        public void Inject(
            [NotNull] IGoToMainMenuUseCase goToMainMenuUseCase,
            [NotNull] IRestartGameplayUseCase restartGameplayUseCase,
            [NotNull] IResumeGameplayUseCase resumeGameplayUseCase)
        {
            ArgumentNullException.ThrowIfNull(goToMainMenuUseCase);
            ArgumentNullException.ThrowIfNull(restartGameplayUseCase);
            ArgumentNullException.ThrowIfNull(resumeGameplayUseCase);

            _goToMainMenuUseCase = goToMainMenuUseCase;
            _restartGameplayUseCase = restartGameplayUseCase;
            _resumeGameplayUseCase = resumeGameplayUseCase;
        }

        private void InitializeBindings()
        {
            _goToMainMenuButtonViewData.Value = new ButtonViewData(HandleGoToMainMenuButtonClick);
            _restartGameplayButtonViewData.Value = new ButtonViewData(HandleRestartGameplayButtonClick);
            _resumeGameplayButtonViewData.Value = new ButtonViewData(HandleResumeGameplayButtonClick);
        }

        private void AddBindings()
        {
            Add(_goToMainMenuButtonViewData);
            Add(_restartGameplayButtonViewData);
            Add(_resumeGameplayButtonViewData);
        }

        private void HandleGoToMainMenuButtonClick()
        {
            InvalidOperationException.ThrowIfNull(_goToMainMenuUseCase);

            _goToMainMenuUseCase.Resolve();
        }

        private void HandleRestartGameplayButtonClick()
        {
            InvalidOperationException.ThrowIfNull(_restartGameplayUseCase);

            _restartGameplayUseCase.Resolve("Test"); // TODO
        }

        private void HandleResumeGameplayButtonClick()
        {
            InvalidOperationException.ThrowIfNull(_resumeGameplayUseCase);

            _resumeGameplayUseCase.Resolve();
        }
    }
}