using System.Collections.Generic;
using Game.Gameplay.Bag;
using Game.Gameplay.Board;
using Game.Gameplay.Camera;
using Game.Gameplay.Goals;
using Game.Gameplay.Moves;
using Game.Gameplay.Parsing;
using Game.Gameplay.Phases;
using Game.Gameplay.Pieces;
using Game.Gameplay.REMOVE;
using Game.Gameplay.View;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Camera;
using Game.Gameplay.View.EventResolvers;
using Game.Gameplay.View.Goals;
using Game.Gameplay.View.Moves;
using Game.Gameplay.View.Pieces;
using Game.Gameplay.View.Player;
using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.UseCases
{
    public class LoadGameplayUseCase : ILoadGameplayUseCase
    {
        [NotNull] private readonly IGameplayDefinitionGetter _gameplayDefinitionGetter;
        [NotNull] private readonly IBagContainer _bagContainer;
        [NotNull] private readonly IBoardContainer _boardContainer;
        [NotNull] private readonly IPieceIdGetter _pieceIdGetter;
        [NotNull] private readonly ICamera _camera;
        [NotNull] private readonly IGoalsContainer _goalsContainer;
        [NotNull] private readonly IMovesContainer _movesContainer;
        [NotNull] private readonly IGameplayParser _gameplayParser;
        [NotNull] private readonly IPhaseContainer _phaseContainer;
        [NotNull] private readonly IGameplaySerializerOnBeginIteration _gameplaySerializerOnBeginIteration;
        [NotNull] private readonly IBoardView _boardView;
        [NotNull] private readonly ICameraView _cameraView;
        [NotNull] private readonly IGoalsView _goalsView;
        [NotNull] private readonly IMovesView _movesView;
        [NotNull] private readonly IPieceGameObjectPreloader _pieceGameObjectPreloader;
        [NotNull] private readonly IPlayerPieceGhostView _playerPieceGhostView;
        [NotNull] private readonly IPlayerPieceView _playerPieceView;
        [NotNull] private readonly IEventsResolver _eventsResolver;
        [NotNull] private readonly IScreenLoader _screenLoader;

        public LoadGameplayUseCase(
            [NotNull] IGameplayDefinitionGetter gameplayDefinitionGetter,
            [NotNull] IBagContainer bagContainer,
            [NotNull] IBoardContainer boardContainer,
            [NotNull] IPieceIdGetter pieceIdGetter,
            [NotNull] ICamera camera,
            [NotNull] IGoalsContainer goalsContainer,
            [NotNull] IMovesContainer movesContainer,
            [NotNull] IGameplayParser gameplayParser,
            [NotNull] IPhaseContainer phaseContainer,
            [NotNull] IGameplaySerializerOnBeginIteration gameplaySerializerOnBeginIteration,
            [NotNull] IBoardView boardView,
            [NotNull] ICameraView cameraView,
            [NotNull] IGoalsView goalsView,
            [NotNull] IMovesView movesView,
            [NotNull] IPieceGameObjectPreloader pieceGameObjectPreloader,
            [NotNull] IPlayerPieceGhostView playerPieceGhostView,
            [NotNull] IPlayerPieceView playerPieceView,
            [NotNull] IEventsResolver eventsResolver,
            [NotNull] IScreenLoader screenLoader)
        {
            ArgumentNullException.ThrowIfNull(gameplayDefinitionGetter);
            ArgumentNullException.ThrowIfNull(bagContainer);
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(pieceIdGetter);
            ArgumentNullException.ThrowIfNull(camera);
            ArgumentNullException.ThrowIfNull(goalsContainer);
            ArgumentNullException.ThrowIfNull(movesContainer);
            ArgumentNullException.ThrowIfNull(gameplayParser);
            ArgumentNullException.ThrowIfNull(phaseContainer);
            ArgumentNullException.ThrowIfNull(gameplaySerializerOnBeginIteration);
            ArgumentNullException.ThrowIfNull(boardView);
            ArgumentNullException.ThrowIfNull(cameraView);
            ArgumentNullException.ThrowIfNull(goalsView);
            ArgumentNullException.ThrowIfNull(movesView);
            ArgumentNullException.ThrowIfNull(pieceGameObjectPreloader);
            ArgumentNullException.ThrowIfNull(playerPieceGhostView);
            ArgumentNullException.ThrowIfNull(playerPieceView);
            ArgumentNullException.ThrowIfNull(eventsResolver);
            ArgumentNullException.ThrowIfNull(screenLoader);

            _gameplayDefinitionGetter = gameplayDefinitionGetter;
            _bagContainer = bagContainer;
            _boardContainer = boardContainer;
            _pieceIdGetter = pieceIdGetter;
            _camera = camera;
            _goalsContainer = goalsContainer;
            _movesContainer = movesContainer;
            _gameplayParser = gameplayParser;
            _phaseContainer = phaseContainer;
            _gameplaySerializerOnBeginIteration = gameplaySerializerOnBeginIteration;
            _boardView = boardView;
            _cameraView = cameraView;
            _goalsView = goalsView;
            _movesView = movesView;
            _pieceGameObjectPreloader = pieceGameObjectPreloader;
            _playerPieceGhostView = playerPieceGhostView;
            _playerPieceView = playerPieceView;
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
                out IGoals goals,
                out IMoves moves,
                out IBag bag
            );

            _bagContainer.Initialize(bag);
            _boardContainer.Initialize(board, piecePlacements);
            _camera.Initialize();
            _goalsContainer.Initialize(goals);
            _movesContainer.Initialize(moves);
            _phaseContainer.Initialize();
            _gameplaySerializerOnBeginIteration.Initialize();
        }

        private void PrepareView()
        {
            _boardView.Initialize();
            _cameraView.Initialize();
            _eventsResolver.Initialize();
            _goalsView.Initialize();
            _movesView.Initialize();
            _playerPieceGhostView.Initialize();
            _playerPieceView.Initialize();

            _pieceGameObjectPreloader.Preload();
        }

        private void LoadScreen()
        {
            GameplayViewData gameplayViewData = new(OnReady);

            _screenLoader.Load(GameplayConstants.ScreenKey, gameplayViewData);
        }

        private void OnReady()
        {
            _phaseContainer.Resolve(new ResolveContext(false, null, null));
        }
    }
}