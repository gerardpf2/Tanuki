using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Parsing;
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
    public class LoadGameplayUseCase : ILoadGameplayUseCase
    {
        [NotNull] private readonly IBoardParser _boardParser;
        [NotNull] private readonly IBoardDefinitionGetter _boardDefinitionGetter;
        [NotNull] private readonly IPhaseResolver _phaseResolver;
        [NotNull] private readonly IPlayerPiecesBag _playerPiecesBag;
        [NotNull] private readonly IBoardView _boardView;
        [NotNull] private readonly IPlayerView _playerView;
        [NotNull] private readonly ICameraController _cameraController;
        [NotNull] private readonly IEventListener _eventListener;
        [NotNull] private readonly IScreenLoader _screenLoader;

        public LoadGameplayUseCase(
            [NotNull] IBoardParser boardParser,
            [NotNull] IBoardDefinitionGetter boardDefinitionGetter,
            [NotNull] IPhaseResolver phaseResolver,
            [NotNull] IPlayerPiecesBag playerPiecesBag,
            [NotNull] IBoardView boardView,
            [NotNull] IPlayerView playerView,
            [NotNull] ICameraController cameraController,
            [NotNull] IEventListener eventListener,
            [NotNull] IScreenLoader screenLoader)
        {
            ArgumentNullException.ThrowIfNull(boardParser);
            ArgumentNullException.ThrowIfNull(boardDefinitionGetter);
            ArgumentNullException.ThrowIfNull(phaseResolver);
            ArgumentNullException.ThrowIfNull(playerPiecesBag);
            ArgumentNullException.ThrowIfNull(boardView);
            ArgumentNullException.ThrowIfNull(playerView);
            ArgumentNullException.ThrowIfNull(cameraController);
            ArgumentNullException.ThrowIfNull(eventListener);
            ArgumentNullException.ThrowIfNull(screenLoader);

            _boardParser = boardParser;
            _boardDefinitionGetter = boardDefinitionGetter;
            _phaseResolver = phaseResolver;
            _playerPiecesBag = playerPiecesBag;
            _boardView = boardView;
            _playerView = playerView;
            _cameraController = cameraController;
            _eventListener = eventListener;
            _screenLoader = screenLoader;
        }

        public void Resolve(string boardId)
        {
            IReadonlyBoard board = PrepareModel(boardId);
            GameplayViewData gameplayViewData = PrepareView(board);

            LoadScreen(gameplayViewData);
        }

        private IReadonlyBoard PrepareModel(string boardId)
        {
            IBoardDefinition boardDefinition = _boardDefinitionGetter.Get(boardId);

            _boardParser.Deserialize(
                boardDefinition.SerializedData,
                out IBoard board,
                out IEnumerable<PiecePlacement> piecePlacements
            );

            _phaseResolver.Initialize(board, piecePlacements);
            _playerPiecesBag.Initialize();

            _phaseResolver.Resolve(new ResolveContext(null));

            return board;
        }

        private GameplayViewData PrepareView(IReadonlyBoard board)
        {
            _boardView.Initialize(board);
            _playerView.Initialize();
            _cameraController.Initialize();

            BoardViewData boardViewData = new(_eventListener.Initialize);
            GameplayViewData gameplayViewData = new(boardViewData);

            return gameplayViewData;
        }

        private void LoadScreen(GameplayViewData gameplayViewData)
        {
            _screenLoader.Load("Gameplay", gameplayViewData);
        }
    }
}