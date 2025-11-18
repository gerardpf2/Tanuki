using Game.Gameplay.Camera;
using Game.Gameplay.Parsing;
using Game.Gameplay.Phases;
using Game.Gameplay.Pieces;
using Game.Gameplay.REMOVE;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Camera;
using Game.Gameplay.View.EventResolvers;
using Game.Gameplay.View.Goals;
using Game.Gameplay.View.Moves;
using Game.Gameplay.View.Pieces.Preloader;
using Game.Gameplay.View.Player;
using Game.Gameplay.View.Player.Input.ActionHandlers;
using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.UseCases
{
    public class LoadGameplayUseCase : ILoadGameplayUseCase
    {
        [NotNull] private readonly IGameplayDefinitionGetter _gameplayDefinitionGetter;
        [NotNull] private readonly IPieceIdGetter _pieceIdGetter;
        [NotNull] private readonly ICamera _camera;
        [NotNull] private readonly IGameplayParser _gameplayParser;
        [NotNull] private readonly IPhaseContainer _phaseContainerInitial;
        [NotNull] private readonly IGameplaySerializerOnBeginIteration _gameplaySerializerOnBeginIteration;
        [NotNull] private readonly IBoardView _boardView;
        [NotNull] private readonly ICameraView _cameraView;
        [NotNull] private readonly IGoalsView _goalsView;
        [NotNull] private readonly IMovesView _movesView;
        [NotNull] private readonly IPieceGameObjectPreloader _pieceGameObjectPreloader;
        [NotNull] private readonly IPlayerInputActionHandler _lockPlayerInputActionHandler;
        [NotNull] private readonly IPlayerInputActionHandler _moveLeftPlayerInputActionHandler;
        [NotNull] private readonly IPlayerInputActionHandler _moveRightPlayerInputActionHandler;
        [NotNull] private readonly IPlayerInputActionHandler _rotatePlayerInputActionHandler;
        [NotNull] private readonly IPlayerPieceGhostView _playerPieceGhostView;
        [NotNull] private readonly IPlayerPieceView _playerPieceView;
        [NotNull] private readonly IEventsResolver _eventsResolver;
        [NotNull] private readonly IScreenLoader _screenLoader;

        public LoadGameplayUseCase(
            [NotNull] IGameplayDefinitionGetter gameplayDefinitionGetter,
            [NotNull] IPieceIdGetter pieceIdGetter,
            [NotNull] ICamera camera,
            [NotNull] IGameplayParser gameplayParser,
            [NotNull] IPhaseContainer phaseContainerInitial,
            [NotNull] IGameplaySerializerOnBeginIteration gameplaySerializerOnBeginIteration,
            [NotNull] IBoardView boardView,
            [NotNull] ICameraView cameraView,
            [NotNull] IGoalsView goalsView,
            [NotNull] IMovesView movesView,
            [NotNull] IPieceGameObjectPreloader pieceGameObjectPreloader,
            [NotNull] IPlayerInputActionHandler lockPlayerInputActionHandler,
            [NotNull] IPlayerInputActionHandler moveLeftPlayerInputActionHandler,
            [NotNull] IPlayerInputActionHandler moveRightPlayerInputActionHandler,
            [NotNull] IPlayerInputActionHandler rotatePlayerInputActionHandler,
            [NotNull] IPlayerPieceGhostView playerPieceGhostView,
            [NotNull] IPlayerPieceView playerPieceView,
            [NotNull] IEventsResolver eventsResolver,
            [NotNull] IScreenLoader screenLoader)
        {
            ArgumentNullException.ThrowIfNull(gameplayDefinitionGetter);
            ArgumentNullException.ThrowIfNull(pieceIdGetter);
            ArgumentNullException.ThrowIfNull(camera);
            ArgumentNullException.ThrowIfNull(gameplayParser);
            ArgumentNullException.ThrowIfNull(phaseContainerInitial);
            ArgumentNullException.ThrowIfNull(gameplaySerializerOnBeginIteration);
            ArgumentNullException.ThrowIfNull(boardView);
            ArgumentNullException.ThrowIfNull(cameraView);
            ArgumentNullException.ThrowIfNull(goalsView);
            ArgumentNullException.ThrowIfNull(movesView);
            ArgumentNullException.ThrowIfNull(pieceGameObjectPreloader);
            ArgumentNullException.ThrowIfNull(lockPlayerInputActionHandler);
            ArgumentNullException.ThrowIfNull(moveLeftPlayerInputActionHandler);
            ArgumentNullException.ThrowIfNull(moveRightPlayerInputActionHandler);
            ArgumentNullException.ThrowIfNull(rotatePlayerInputActionHandler);
            ArgumentNullException.ThrowIfNull(playerPieceGhostView);
            ArgumentNullException.ThrowIfNull(playerPieceView);
            ArgumentNullException.ThrowIfNull(eventsResolver);
            ArgumentNullException.ThrowIfNull(screenLoader);

            _gameplayDefinitionGetter = gameplayDefinitionGetter;
            _pieceIdGetter = pieceIdGetter;
            _camera = camera;
            _gameplayParser = gameplayParser;
            _phaseContainerInitial = phaseContainerInitial;
            _gameplaySerializerOnBeginIteration = gameplaySerializerOnBeginIteration;
            _boardView = boardView;
            _cameraView = cameraView;
            _goalsView = goalsView;
            _movesView = movesView;
            _pieceGameObjectPreloader = pieceGameObjectPreloader;
            _lockPlayerInputActionHandler = lockPlayerInputActionHandler;
            _moveLeftPlayerInputActionHandler = moveLeftPlayerInputActionHandler;
            _moveRightPlayerInputActionHandler = moveRightPlayerInputActionHandler;
            _rotatePlayerInputActionHandler = rotatePlayerInputActionHandler;
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

            _gameplayParser.Deserialize(gameplayDefinition.Data);

            _camera.Initialize();
            _gameplaySerializerOnBeginIteration.Initialize();
        }

        private void PrepareView()
        {
            _boardView.Initialize();
            _cameraView.Initialize();
            _eventsResolver.Initialize();
            _goalsView.Initialize();
            _movesView.Initialize();
            _lockPlayerInputActionHandler.Initialize();
            _moveLeftPlayerInputActionHandler.Initialize();
            _moveRightPlayerInputActionHandler.Initialize();
            _rotatePlayerInputActionHandler.Initialize();
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
            _phaseContainerInitial.Resolve(new ResolveContext(null, null));
        }
    }
}