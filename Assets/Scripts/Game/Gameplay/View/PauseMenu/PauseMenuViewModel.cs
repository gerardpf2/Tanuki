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

        [NotNull] private readonly IBoundProperty<ButtonViewData> _resumeGameplayButtonViewData = new BoundProperty<ButtonViewData>("ResumeGameplayButtonViewData");
        [NotNull] private readonly IBoundProperty<ButtonViewData> _restartGameplayButtonViewData = new BoundProperty<ButtonViewData>("RestartGameplayButtonViewData");
        [NotNull] private readonly IBoundProperty<ButtonViewData> _goToMainMenuButtonViewData = new BoundProperty<ButtonViewData>("GoToMainMenuButtonViewData");

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
            _resumeGameplayButtonViewData.Value = new ButtonViewData(HandleResumeGameplayButtonClick);
            _restartGameplayButtonViewData.Value = new ButtonViewData(HandleRestartGameplayButtonClick);
            _goToMainMenuButtonViewData.Value = new ButtonViewData(HandleGoToMainMenuButtonClick);
        }

        private void AddBindings()
        {
            Add(_resumeGameplayButtonViewData);
            Add(_restartGameplayButtonViewData);
            Add(_goToMainMenuButtonViewData);
        }

        private void HandleResumeGameplayButtonClick()
        {
            InvalidOperationException.ThrowIfNull(_resumeGameplayUseCase);

            _resumeGameplayUseCase.Resolve();
        }

        private static void HandleRestartGameplayButtonClick()
        {
            // TODO
        }

        private void HandleGoToMainMenuButtonClick()
        {
            InvalidOperationException.ThrowIfNull(_goToMainMenuUseCase);

            _goToMainMenuUseCase.Resolve();
        }
    }
}