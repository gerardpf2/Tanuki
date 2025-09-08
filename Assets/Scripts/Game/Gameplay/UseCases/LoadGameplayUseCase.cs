using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Parsing;
using Game.Gameplay.Goals;
using Game.Gameplay.PhaseResolution;
using Game.Gameplay.Player;
using Game.Gameplay.View;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Camera;
using Game.Gameplay.View.EventResolution;
using Game.Gameplay.View.Header.Goals;
using Game.Gameplay.View.Player;
using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.UseCases
{
    public class LoadGameplayUseCase : ILoadGameplayUseCase
    {
        [NotNull] private readonly IUnloadGameplayUseCase _unloadGameplayUseCase;
        [NotNull] private readonly IBoardParser _boardParser;
        [NotNull] private readonly IGameplayDefinitionGetter _gameplayDefinitionGetter;
        [NotNull] private readonly IGoalsContainer _goalsContainer;
        [NotNull] private readonly IPhaseResolver _phaseResolver;
        [NotNull] private readonly IPlayerPiecesBag _playerPiecesBag;
        [NotNull] private readonly IBoardView _boardView;
        [NotNull] private readonly IGoalsViewContainer _goalsViewContainer;
        [NotNull] private readonly IPlayerView _playerView;
        [NotNull] private readonly ICameraController _cameraController;
        [NotNull] private readonly IEventListener _eventListener;
        [NotNull] private readonly IScreenLoader _screenLoader;

        public LoadGameplayUseCase(
            [NotNull] IUnloadGameplayUseCase unloadGameplayUseCase,
            [NotNull] IBoardParser boardParser,
            [NotNull] IGameplayDefinitionGetter gameplayDefinitionGetter,
            [NotNull] IGoalsContainer goalsContainer,
            [NotNull] IPhaseResolver phaseResolver,
            [NotNull] IPlayerPiecesBag playerPiecesBag,
            [NotNull] IBoardView boardView,
            [NotNull] IGoalsViewContainer goalsViewContainer,
            [NotNull] IPlayerView playerView,
            [NotNull] ICameraController cameraController,
            [NotNull] IEventListener eventListener,
            [NotNull] IScreenLoader screenLoader)
        {
            ArgumentNullException.ThrowIfNull(unloadGameplayUseCase);
            ArgumentNullException.ThrowIfNull(boardParser);
            ArgumentNullException.ThrowIfNull(gameplayDefinitionGetter);
            ArgumentNullException.ThrowIfNull(goalsContainer);
            ArgumentNullException.ThrowIfNull(phaseResolver);
            ArgumentNullException.ThrowIfNull(playerPiecesBag);
            ArgumentNullException.ThrowIfNull(boardView);
            ArgumentNullException.ThrowIfNull(goalsViewContainer);
            ArgumentNullException.ThrowIfNull(playerView);
            ArgumentNullException.ThrowIfNull(cameraController);
            ArgumentNullException.ThrowIfNull(eventListener);
            ArgumentNullException.ThrowIfNull(screenLoader);

            _unloadGameplayUseCase = unloadGameplayUseCase;
            _boardParser = boardParser;
            _gameplayDefinitionGetter = gameplayDefinitionGetter;
            _goalsContainer = goalsContainer;
            _phaseResolver = phaseResolver;
            _playerPiecesBag = playerPiecesBag;
            _boardView = boardView;
            _goalsViewContainer = goalsViewContainer;
            _playerView = playerView;
            _cameraController = cameraController;
            _eventListener = eventListener;
            _screenLoader = screenLoader;
        }

        public void Resolve(string id)
        {
            _unloadGameplayUseCase.Resolve();

            (IReadonlyBoard board, IEnumerable<IGoalDefinition> initialGoalDefinitions) = PrepareModel(id);
            GameplayViewData gameplayViewData = PrepareView(board, initialGoalDefinitions);

            LoadScreen(gameplayViewData);
        }

        private (IReadonlyBoard, IEnumerable<IGoalDefinition>) PrepareModel(string id)
        {
            IGameplayDefinition gameplayDefinition = _gameplayDefinitionGetter.Get(id);

            _boardParser.Deserialize(
                gameplayDefinition.BoardDefinition.SerializedData,
                out IBoard board,
                out IEnumerable<PiecePlacement> piecePlacements
            );

            _goalsContainer.Initialize(gameplayDefinition.GoalDefinitions);
            _phaseResolver.Initialize(board, piecePlacements);
            _playerPiecesBag.Initialize();

            _phaseResolver.Resolve(new ResolveContext(null));

            return (board, gameplayDefinition.GoalDefinitions);
        }

        private GameplayViewData PrepareView(IReadonlyBoard board, IEnumerable<IGoalDefinition> initialGoalDefinitions)
        {
            _boardView.Initialize(board);
            _goalsViewContainer.Initialize(initialGoalDefinitions);
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