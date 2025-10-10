using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Goals;
using Game.Gameplay.Parsing;
using Game.Gameplay.PhaseResolution;
using Game.Gameplay.Player;
using Game.Gameplay.View;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Camera;
using Game.Gameplay.View.EventResolution;
using Game.Gameplay.View.Header.Goals;
using Game.Gameplay.View.Input;
using Game.Gameplay.View.Player;
using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.UseCases
{
    public class LoadGameplayUseCase : ILoadGameplayUseCase
    {
        [NotNull] private readonly IGameplayDefinitionGetter _gameplayDefinitionGetter;
        [NotNull] private readonly IBoardContainer _boardContainer;
        [NotNull] private readonly IPieceIdGetter _pieceIdGetter;
        [NotNull] private readonly IGoalsContainer _goalsContainer;
        [NotNull] private readonly IGameplayParser _gameplayParser;
        [NotNull] private readonly IPhaseResolver _phaseResolver;
        [NotNull] private readonly IPlayerPiecesBag _playerPiecesBag;
        [NotNull] private readonly IBoardView _boardView;
        [NotNull] private readonly ICameraView _cameraView;
        [NotNull] private readonly IGoalsView _goalsView;
        [NotNull] private readonly IInputHandler _inputHandler;
        [NotNull] private readonly IPlayerView _playerView;
        [NotNull] private readonly IEventsResolver _eventsResolver;
        [NotNull] private readonly IScreenLoader _screenLoader;

        public LoadGameplayUseCase(
            [NotNull] IGameplayDefinitionGetter gameplayDefinitionGetter,
            [NotNull] IBoardContainer boardContainer,
            [NotNull] IPieceIdGetter pieceIdGetter,
            [NotNull] IGoalsContainer goalsContainer,
            [NotNull] IGameplayParser gameplayParser,
            [NotNull] IPhaseResolver phaseResolver,
            [NotNull] IPlayerPiecesBag playerPiecesBag,
            [NotNull] IBoardView boardView,
            [NotNull] ICameraView cameraView,
            [NotNull] IGoalsView goalsView,
            [NotNull] IInputHandler inputHandler,
            [NotNull] IPlayerView playerView,
            [NotNull] IEventsResolver eventsResolver,
            [NotNull] IScreenLoader screenLoader)
        {
            ArgumentNullException.ThrowIfNull(gameplayDefinitionGetter);
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(pieceIdGetter);
            ArgumentNullException.ThrowIfNull(goalsContainer);
            ArgumentNullException.ThrowIfNull(gameplayParser);
            ArgumentNullException.ThrowIfNull(phaseResolver);
            ArgumentNullException.ThrowIfNull(playerPiecesBag);
            ArgumentNullException.ThrowIfNull(boardView);
            ArgumentNullException.ThrowIfNull(cameraView);
            ArgumentNullException.ThrowIfNull(goalsView);
            ArgumentNullException.ThrowIfNull(inputHandler);
            ArgumentNullException.ThrowIfNull(playerView);
            ArgumentNullException.ThrowIfNull(eventsResolver);
            ArgumentNullException.ThrowIfNull(screenLoader);

            _gameplayDefinitionGetter = gameplayDefinitionGetter;
            _boardContainer = boardContainer;
            _pieceIdGetter = pieceIdGetter;
            _goalsContainer = goalsContainer;
            _gameplayParser = gameplayParser;
            _phaseResolver = phaseResolver;
            _playerPiecesBag = playerPiecesBag;
            _boardView = boardView;
            _cameraView = cameraView;
            _goalsView = goalsView;
            _inputHandler = inputHandler;
            _playerView = playerView;
            _eventsResolver = eventsResolver;
            _screenLoader = screenLoader;
        }

        public void Resolve(string id)
        {
            PrepareModel(id);
            PrepareView();
            LoadScreen();
        }

        private void PrepareModel(string id)
        {
            _pieceIdGetter.Initialize();

            IGameplayDefinition gameplayDefinition = _gameplayDefinitionGetter.Get(id);

            _gameplayParser.Deserialize(
                gameplayDefinition.Data,
                out IBoard board,
                out IEnumerable<PiecePlacement> piecePlacements,
                out IGoals goals
            );

            _boardContainer.Initialize(board, piecePlacements);
            _goalsContainer.Initialize(goals);
            _phaseResolver.Initialize();
            _playerPiecesBag.Initialize();
        }

        private void PrepareView()
        {
            _boardView.Initialize();
            _cameraView.Initialize();
            _eventsResolver.Initialize();
            _goalsView.Initialize();
            _inputHandler.Initialize();
            _playerView.Initialize();
        }

        private void LoadScreen()
        {
            GameplayViewData gameplayViewData = new(OnReady);

            _screenLoader.Load(GameplayConstants.ScreenKey, gameplayViewData);
        }

        private void OnReady()
        {
            _phaseResolver.Resolve(new ResolveContext(null));
        }
    }
}