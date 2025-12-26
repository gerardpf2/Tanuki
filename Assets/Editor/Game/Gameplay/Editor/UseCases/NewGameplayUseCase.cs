using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Editor.Game.Gameplay.Editor.UseCases
{
    public class NewGameplayUseCase : INewGameplayUseCase
    {
        [NotNull] private readonly IShowNewGameplayPopupUseCase _showNewGameplayPopupUseCase;

        public NewGameplayUseCase([NotNull] IShowNewGameplayPopupUseCase showNewGameplayPopupUseCase)
        {
            ArgumentNullException.ThrowIfNull(showNewGameplayPopupUseCase);

            _showNewGameplayPopupUseCase = showNewGameplayPopupUseCase;
        }

        public void Resolve()
        {
            _showNewGameplayPopupUseCase.Resolve();
        }
    }
}