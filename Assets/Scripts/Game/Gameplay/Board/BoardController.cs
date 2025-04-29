using System.Collections.Generic;
using Game.Gameplay.PhaseResolution;
using Game.Gameplay.View.EventResolution;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Board
{
    public class BoardController : IBoardController
    {
        [NotNull] private readonly IBoardDefinitionGetter _boardDefinitionGetter;
        [NotNull] private readonly IPhaseResolver _phaseResolver;
        [NotNull] private readonly IEventListener _eventListener;

        private Board _board;
        private IEnumerable<IPiecePlacement> _piecePlacements;

        public int Rows
        {
            get
            {
                InvalidOperationException.ThrowIfNull(_board);

                return _board.Rows;
            }
        }

        public int Columns
        {
            get
            {
                InvalidOperationException.ThrowIfNull(_board);

                return _board.Columns;
            }
        }

        public BoardController(
            [NotNull] IBoardDefinitionGetter boardDefinitionGetter,
            [NotNull] IPhaseResolver phaseResolver,
            [NotNull] IEventListener eventListener)
        {
            ArgumentNullException.ThrowIfNull(boardDefinitionGetter);
            ArgumentNullException.ThrowIfNull(phaseResolver);
            ArgumentNullException.ThrowIfNull(eventListener);

            _boardDefinitionGetter = boardDefinitionGetter;
            _phaseResolver = phaseResolver;
            _eventListener = eventListener;
        }

        public void Initialize(string boardId)
        {
            IBoardDefinition boardDefinition = _boardDefinitionGetter.Get(boardId);

            _board = new Board(boardDefinition.Rows, boardDefinition.Columns);
            _piecePlacements = boardDefinition.PiecePlacements;

            _eventListener.Initialize();
        }

        public void ResolveInstantiateInitialPhase()
        {
            _phaseResolver.ResolveInstantiateInitial(_board, _piecePlacements);
        }
    }
}