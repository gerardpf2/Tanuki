using Editor.Game.Gameplay.Editor.TopMenu;
using Game.Common.View.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Editor.Game.Gameplay.Editor.UseCases
{
    public class LaunchGameplayEditorUseCase : ILaunchGameplayEditorUseCase
    {
        [NotNull] private readonly IGameplayEditorParametersGetter _gameplayEditorParametersGetter; // TODO
        [NotNull] private readonly PieceSpriteContainer _pieceSpriteContainer; // TODO

        public LaunchGameplayEditorUseCase(
            [NotNull] IGameplayEditorParametersGetter gameplayEditorParametersGetter,
            [NotNull] PieceSpriteContainer pieceSpriteContainer)
        {
            ArgumentNullException.ThrowIfNull(gameplayEditorParametersGetter);
            ArgumentNullException.ThrowIfNull(pieceSpriteContainer);

            _gameplayEditorParametersGetter = gameplayEditorParametersGetter;
            _pieceSpriteContainer = pieceSpriteContainer;
        }

        public void Resolve()
        {
            /*
             *
             * Dependencies should be ctor params, but this class is an exception and creates them in here
             * It is expected to be used by GameplayEditorLauncher in order to simplify its code
             *
             */

            IClearGameplayUseCase clearGameplayUseCase = new ClearGameplayUseCase();

            IGameplayEditorTopMenu gameplayEditorTopMenu = new GameplayEditorTopMenu(clearGameplayUseCase);

            IShowGameplayEditorUseCase showGameplayEditorUseCase = new ShowGameplayEditorUseCase(gameplayEditorTopMenu);

            showGameplayEditorUseCase.Resolve();
        }
    }
}