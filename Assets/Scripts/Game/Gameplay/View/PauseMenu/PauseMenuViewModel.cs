using Infrastructure.ModelViewViewModel;
using Infrastructure.ModelViewViewModel.Examples.Button;
using JetBrains.Annotations;

namespace Game.Gameplay.View.PauseMenu
{
    public class PauseMenuViewModel : ViewModel
    {
        [NotNull] private readonly IBoundProperty<ButtonViewData> _resumeButtonViewData = new BoundProperty<ButtonViewData>("ResumeButtonViewData");
        [NotNull] private readonly IBoundProperty<ButtonViewData> _restartButtonViewData = new BoundProperty<ButtonViewData>("RestartButtonViewData");
        [NotNull] private readonly IBoundProperty<ButtonViewData> _mainMenuButtonViewData = new BoundProperty<ButtonViewData>("MainMenuButtonViewData");

        private void Awake()
        {
            InitializeBindings();
            AddBindings();
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

        private static void HandleResumeButtonClick()
        {
            // TODO
        }

        private static void HandleRestartButtonClick()
        {
            // TODO
        }

        private static void HandleMainMenuButtonClick()
        {
            // TODO
        }
    }
}