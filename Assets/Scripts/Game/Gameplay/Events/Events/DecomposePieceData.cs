using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Pieces;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Events.Events
{
    public class DecomposePieceData
    {
        [NotNull, ItemNotNull] private readonly ICollection<PiecePlacement> _piecePlacements = new List<PiecePlacement>(); // ItemNotNull as long as all Add check for null

        [NotNull, ItemNotNull]
        public IEnumerable<PiecePlacement> PiecePlacements => _piecePlacements;

        public void Add([NotNull] IPiece piece, Coordinate sourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(piece);

            PiecePlacement piecePlacement =
                new(
                    piece.Clone(), // Clone needed so model and view boards have different piece refs
                    sourceCoordinate
                );

            _piecePlacements.Add(piecePlacement);
        }
    }
}