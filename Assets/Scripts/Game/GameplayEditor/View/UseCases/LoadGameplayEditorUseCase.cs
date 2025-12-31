using Game.Gameplay;
using Game.Gameplay.Camera;
using Game.Gameplay.Parsing;
using Game.Gameplay.Phases;
using Game.Gameplay.Pieces;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Camera;
using Game.Gameplay.View.EventResolvers;
using Game.Gameplay.View.Goals;
using Game.Gameplay.View.Moves;
using Game.Gameplay.View.Player;
using Game.Gameplay.View.Player.Input.ActionHandlers;
using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.GameplayEditor.View.UseCases
{
    public class LoadGameplayEditorUseCase : ILoadGameplayEditorUseCase
    {
        // TODO: Dependencies

        [NotNull] private readonly IGameplayDefinitionGetter _gameplayDefinitionGetter;
        [NotNull] private readonly IPieceIdGetter _pieceIdGetter;
        [NotNull] private readonly ICamera _camera;
        [NotNull] private readonly IGameplayParser _gameplayParser;
        [NotNull] private readonly IPhaseContainer _phaseContainerInitial;
        [NotNull] private readonly IBoardView _boardView;
        [NotNull] private readonly ICameraView _cameraView;
        [NotNull] private readonly IGoalsView _goalsView;
        [NotNull] private readonly IMovesView _movesView;
        [NotNull] private readonly IPlayerInputActionHandler _lockPlayerInputActionHandler;
        [NotNull] private readonly IPlayerInputActionHandler _moveLeftPlayerInputActionHandler;
        [NotNull] private readonly IPlayerInputActionHandler _moveRightPlayerInputActionHandler;
        [NotNull] private readonly IPlayerInputActionHandler _rotatePlayerInputActionHandler;
        [NotNull] private readonly IPlayerPieceGhostView _playerPieceGhostView;
        [NotNull] private readonly IPlayerPieceView _playerPieceView;
        [NotNull] private readonly IEventsResolver _eventsResolver;
        [NotNull] private readonly IScreenLoader _screenLoader;

        public LoadGameplayEditorUseCase(
            [NotNull] IGameplayDefinitionGetter gameplayDefinitionGetter,
            [NotNull] IPieceIdGetter pieceIdGetter,
            [NotNull] ICamera camera,
            [NotNull] IGameplayParser gameplayParser,
            [NotNull] IPhaseContainer phaseContainerInitial,
            [NotNull] IBoardView boardView,
            [NotNull] ICameraView cameraView,
            [NotNull] IGoalsView goalsView,
            [NotNull] IMovesView movesView,
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
            ArgumentNullException.ThrowIfNull(boardView);
            ArgumentNullException.ThrowIfNull(cameraView);
            ArgumentNullException.ThrowIfNull(goalsView);
            ArgumentNullException.ThrowIfNull(movesView);
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
            _boardView = boardView;
            _cameraView = cameraView;
            _goalsView = goalsView;
            _movesView = movesView;
            _lockPlayerInputActionHandler = lockPlayerInputActionHandler;
            _moveLeftPlayerInputActionHandler = moveLeftPlayerInputActionHandler;
            _moveRightPlayerInputActionHandler = moveRightPlayerInputActionHandler;
            _rotatePlayerInputActionHandler = rotatePlayerInputActionHandler;
            _playerPieceGhostView = playerPieceGhostView;
            _playerPieceView = playerPieceView;
            _eventsResolver = eventsResolver;
            _screenLoader = screenLoader;
        }

        public void Resolve()
        {
            PrepareModel();
            PrepareView();
            LoadScreen();
        }

        private void PrepareModel()
        {
            _pieceIdGetter.Initialize();

            IGameplayDefinition gameplayDefinition = _gameplayDefinitionGetter.Get("Test"); // TODO: Default

            _gameplayParser.Deserialize(gameplayDefinition.Data);

            _camera.Initialize();
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
        }

        private void LoadScreen()
        {
            GameplayEditorViewData gameplayViewData = new(OnReady);

            _screenLoader.Load(GameplayEditorConstants.ScreenKey, gameplayViewData);
        }

        private void OnReady()
        {
            _phaseContainerInitial.Resolve(new ResolveContext(ResolveReason.Load, null, null));
        }
    }
}