using System.Collections.Generic;
using Game.Gameplay.PhaseResolution;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Board
{
    public class BoardController : IBoardController
    {
        [NotNull] private readonly IBoardDefinitionGetter _boardDefinitionGetter;
        [NotNull] private readonly IPhaseResolver _phaseResolver;

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
            [NotNull] IPhaseResolver phaseResolver)
        {
            ArgumentNullException.ThrowIfNull(boardDefinitionGetter);
            ArgumentNullException.ThrowIfNull(phaseResolver);

            _boardDefinitionGetter = boardDefinitionGetter;
            _phaseResolver = phaseResolver;
        }

        public void Initialize(string boardId)
        {
            // TODO: Check allow multiple Initialize. Add Clear ¿?

            IBoardDefinition boardDefinition = _boardDefinitionGetter.Get(boardId);

            _board = new Board(boardDefinition.Rows, boardDefinition.Columns);
            _piecePlacements = boardDefinition.PiecePlacements;
        }

        public void ResolveInstantiateInitialAndCascade()
        {
            _phaseResolver.ResolveInstantiateInitialAndCascade(_board, _piecePlacements);
        }
    }
}