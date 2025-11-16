using System.Collections.Generic;
using Game.Common.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces
{
    public class PieceTypeCategoryRegistry : IPieceTypeCategoryRegistry
    {
        [NotNull] private static readonly IDictionary<PieceType, PieceType> DecomposeBlockTypes =
            new Dictionary<PieceType, PieceType>
            {
                { PieceType.PlayerI, PieceType.BlockI },
                { PieceType.PlayerO, PieceType.BlockO },
                { PieceType.PlayerT, PieceType.BlockT },
                { PieceType.PlayerJ, PieceType.BlockJ },
                { PieceType.PlayerL, PieceType.BlockL },
                { PieceType.PlayerS, PieceType.BlockS },
                { PieceType.PlayerZ, PieceType.BlockZ },
            };

        public PieceType GetDecomposeBlockType(PieceType pieceType)
        {
            if (!DecomposeBlockTypes.TryGetValue(pieceType, out PieceType decomposeBlockType))
            {
                InvalidOperationException.Throw($"Cannot get decompose block type for Type: {pieceType}");
            }

            return decomposeBlockType;
        }
    }
}