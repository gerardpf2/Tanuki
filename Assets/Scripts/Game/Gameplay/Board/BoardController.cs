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

        public BoardController(
            [NotNull] IBoardDefinitionGetter boardDefinitionGetter,
            [NotNull] IPhaseResolver phaseResolver)
        {
            ArgumentNullException.ThrowIfNull(boardDefinitionGetter);
            ArgumentNullException.ThrowIfNull(phaseResolver);

            _boardDefinitionGetter = boardDefinitionGetter;
            _phaseResolver = phaseResolver;
        }

        public IReadonlyBoard Initialize(string boardId)
        {
            // TODO: Check allow multiple Initialize. Add Clear ¿?

            IBoardDefinition boardDefinition = _boardDefinitionGetter.Get(boardId);

            _board = new Board(boardDefinition.Rows, boardDefinition.Columns);
            _piecePlacements = boardDefinition.PiecePlacements;

            return _board;
        }

        public void ResolveInstantiateInitialAndCascade()
        {
            _phaseResolver.ResolveInstantiateInitialAndCascade(_board, _piecePlacements);
        }
    }
}