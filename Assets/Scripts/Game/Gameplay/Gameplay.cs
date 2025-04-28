using Game.Gameplay.Board;
using Game.Gameplay.PhaseResolution;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.EventResolution;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay
{
    public class Gameplay : IGameplay
    {
        [NotNull] private readonly IBoardDefinitionGetter _boardDefinitionGetter;
        [NotNull] private readonly IPhaseResolver _phaseResolver;
        [NotNull] private readonly IEventListener _eventListener;
        [NotNull] private readonly IBoardView _boardView;

        private Board.Board _board;

        public Gameplay(
            [NotNull] IBoardDefinitionGetter boardDefinitionGetter,
            [NotNull] IPhaseResolver phaseResolver,
            [NotNull] IEventListener eventListener,
            [NotNull] IBoardView boardView)
        {
            ArgumentNullException.ThrowIfNull(boardDefinitionGetter);
            ArgumentNullException.ThrowIfNull(phaseResolver);
            ArgumentNullException.ThrowIfNull(eventListener);
            ArgumentNullException.ThrowIfNull(boardView);

            _boardDefinitionGetter = boardDefinitionGetter;
            _phaseResolver = phaseResolver;
            _eventListener = eventListener;
            _boardView = boardView;
        }

        public void Initialize(string boardId)
        {
            IBoardDefinition boardDefinition = _boardDefinitionGetter.Get(boardId);

            int rows = boardDefinition.Rows;
            int columns = boardDefinition.Columns;

            _board = new Board.Board(rows, columns);

            _boardView.Initialize(rows, columns);

            _eventListener.Initialize();

            _phaseResolver.ResolveInstantiateInitial(_board, boardDefinition.PiecePlacements);
        }
    }
}