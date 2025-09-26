using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Parsing;
using Game.Gameplay.Goals;
using Game.Gameplay.Goals.Parsing;
using Game.Gameplay.PhaseResolution;
using Game.Gameplay.Player;
using Game.Gameplay.View;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Camera;
using Game.Gameplay.View.EventResolution;
using Game.Gameplay.View.Player;
using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.UseCases
{
    // TODO: GoalsView
    public class LoadGameplayUseCase : ILoadGameplayUseCase
    {
        [NotNull] private readonly IUnloadGameplayUseCase _unloadGameplayUseCase;
        [NotNull] private readonly IBoardParser _boardParser;
        [NotNull] private readonly IGameplayDefinitionGetter _gameplayDefinitionGetter;
        [NotNull] private readonly IBoardContainer _boardContainer;
        [NotNull] private readonly IGoalsParser _goalsParser;
        [NotNull] private readonly IGoalsContainer _goalsContainer;
        [NotNull] private readonly IPhaseResolver _phaseResolver;
        [NotNull] private readonly IPlayerPiecesBag _playerPiecesBag;
        [NotNull] private readonly IBoardView _boardView;
        [NotNull] private readonly IPlayerView _playerView;
        [NotNull] private readonly ICameraController _cameraController;
        [NotNull] private readonly IEventListener _eventListener;
        [NotNull] private readonly IScreenLoader _screenLoader;

        public LoadGameplayUseCase(
            [NotNull] IUnloadGameplayUseCase unloadGameplayUseCase,
            [NotNull] IBoardParser boardParser,
            [NotNull] IGameplayDefinitionGetter gameplayDefinitionGetter,
            [NotNull] IBoardContainer boardContainer,
            [NotNull] IGoalsParser goalsParser,
            [NotNull] IGoalsContainer goalsContainer,
            [NotNull] IPhaseResolver phaseResolver,
            [NotNull] IPlayerPiecesBag playerPiecesBag,
            [NotNull] IBoardView boardView,
            [NotNull] IPlayerView playerView,
            [NotNull] ICameraController cameraController,
            [NotNull] IEventListener eventListener,
            [NotNull] IScreenLoader screenLoader)
        {
            ArgumentNullException.ThrowIfNull(unloadGameplayUseCase);
            ArgumentNullException.ThrowIfNull(boardParser);
            ArgumentNullException.ThrowIfNull(gameplayDefinitionGetter);
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(goalsParser);
            ArgumentNullException.ThrowIfNull(goalsContainer);
            ArgumentNullException.ThrowIfNull(phaseResolver);
            ArgumentNullException.ThrowIfNull(playerPiecesBag);
            ArgumentNullException.ThrowIfNull(boardView);
            ArgumentNullException.ThrowIfNull(playerView);
            ArgumentNullException.ThrowIfNull(cameraController);
            ArgumentNullException.ThrowIfNull(eventListener);
            ArgumentNullException.ThrowIfNull(screenLoader);

            _unloadGameplayUseCase = unloadGameplayUseCase;
            _boardParser = boardParser;
            _gameplayDefinitionGetter = gameplayDefinitionGetter;
            _boardContainer = boardContainer;
            _goalsParser = goalsParser;
            _goalsContainer = goalsContainer;
            _phaseResolver = phaseResolver;
            _playerPiecesBag = playerPiecesBag;
            _boardView = boardView;
            _playerView = playerView;
            _cameraController = cameraController;
            _eventListener = eventListener;
            _screenLoader = screenLoader;
        }

        public void Resolve(string id)
        {
            _unloadGameplayUseCase.Resolve();

            PrepareModel(id);

            GameplayViewData gameplayViewData = PrepareView();

            LoadScreen(gameplayViewData);
        }

        private void PrepareModel(string id)
        {
            IGameplayDefinition gameplayDefinition = _gameplayDefinitionGetter.Get(id);

            _boardParser.Deserialize(
                gameplayDefinition.Board,
                out IBoard board,
                out IEnumerable<PiecePlacement> piecePlacements
            );

            IGoals goals = _goalsParser.Deserialize(gameplayDefinition.Goals);

            _boardContainer.Initialize(board, piecePlacements);
            _goalsContainer.Initialize(goals);
            _phaseResolver.Initialize();
            _playerPiecesBag.Initialize();

            _phaseResolver.Resolve(new ResolveContext(null));
        }

        private GameplayViewData PrepareView()
        {
            _boardView.Initialize();
            _playerView.Initialize();
            _cameraController.Initialize();

            GameplayViewData gameplayViewData = new(_eventListener.Initialize);

            return gameplayViewData;
        }

        private void LoadScreen(GameplayViewData gameplayViewData)
        {
            _screenLoader.Load(GameplayConstants.ScreenKey, gameplayViewData);
        }
    }
}