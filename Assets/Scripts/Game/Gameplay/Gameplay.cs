using Game.Gameplay.Board;
using Game.Gameplay.PhaseResolution;
using Game.Gameplay.View.Board;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay
{
    public class Gameplay : IGameplay
    {
        [NotNull] private readonly IBoardDefinitionGetter _boardDefinitionGetter;
        [NotNull] private readonly IPhaseResolver _phaseResolver;
        [NotNull] private readonly IBoardView _boardView;

        private Board.Board _board;

        public Gameplay(
            [NotNull] IBoardDefinitionGetter boardDefinitionGetter,
            [NotNull] IPhaseResolver phaseResolver,
            [NotNull] IBoardView boardView)
        {
            ArgumentNullException.ThrowIfNull(boardDefinitionGetter);
            ArgumentNullException.ThrowIfNull(phaseResolver);
            ArgumentNullException.ThrowIfNull(boardView);

            _boardDefinitionGetter = boardDefinitionGetter;
            _phaseResolver = phaseResolver;
            _boardView = boardView;
        }

        public void Initialize(string boardId)
        {
            IBoardDefinition boardDefinition = _boardDefinitionGetter.Get(boardId);

            int rows = boardDefinition.Rows;
            int columns = boardDefinition.Columns;

            _board = new Board.Board(rows, columns);

            _boardView.Initialize(rows, columns);

            _phaseResolver.InitializeAndResolve(_board, boardDefinition.PiecePlacements);
        }
    }
}