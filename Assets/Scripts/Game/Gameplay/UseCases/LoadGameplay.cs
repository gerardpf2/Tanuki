using Game.Gameplay.Board;
using Game.Gameplay.PhaseResolution;
using Game.Gameplay.View;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.EventResolution;
using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.UseCases
{
    public class LoadGameplay : ILoadGameplay
    {
        [NotNull] private readonly IBoardDefinitionGetter _boardDefinitionGetter;
        [NotNull] private readonly IPhaseResolver _phaseResolver;
        [NotNull] private readonly IEventListener _eventListener;
        [NotNull] private readonly IScreenLoader _screenLoader;

        public LoadGameplay(
            [NotNull] IBoardDefinitionGetter boardDefinitionGetter,
            [NotNull] IPhaseResolver phaseResolver,
            [NotNull] IEventListener eventListener,
            [NotNull] IScreenLoader screenLoader)
        {
            ArgumentNullException.ThrowIfNull(boardDefinitionGetter);
            ArgumentNullException.ThrowIfNull(phaseResolver);
            ArgumentNullException.ThrowIfNull(eventListener);
            ArgumentNullException.ThrowIfNull(screenLoader);

            _boardDefinitionGetter = boardDefinitionGetter;
            _phaseResolver = phaseResolver;
            _eventListener = eventListener;
            _screenLoader = screenLoader;
        }

        public void Resolve(string boardId)
        {
            IReadonlyBoard board = PrepareModel(boardId);

            LoadView(board);
        }

        private IReadonlyBoard PrepareModel(string boardId)
        {
            IBoardDefinition boardDefinition = _boardDefinitionGetter.Get(boardId);
            IBoard board = new Board.Board(boardDefinition.Rows, boardDefinition.Columns);

            _phaseResolver.ResolveInstantiateInitialAndCascade(board, boardDefinition.PiecePlacements);

            return board;
        }

        private void LoadView(IReadonlyBoard board)
        {
            BoardViewData boardViewData = new(board, _eventListener.Initialize);
            GameplayViewData gameplayViewData = new(boardViewData);

            _screenLoader.Load("Gameplay", gameplayViewData);
        }
    }
}