using System.Collections.Generic;
using Game.Common.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Pieces
{
    [CreateAssetMenu(fileName = nameof(PieceViewDefinitionContainer), menuName = "Tanuki/Game/Gameplay/Pieces/" + nameof(PieceViewDefinitionContainer))]
    public class PieceViewDefinitionContainer : ScriptableObject, IPieceViewDefinitionGetter
    {
        [SerializeField] private PieceViewDefinition[] _pieceViewDefinitions;
        [SerializeField] private PieceViewDefinition[] _pieceGhostViewDefinitions;

        public IPieceViewDefinition Get(PieceType pieceType)
        {
            InvalidOperationException.ThrowIfNull(_pieceViewDefinitions);

            return Get(_pieceViewDefinitions, pieceType);
        }

        public IPieceViewDefinition GetGhost(PieceType pieceType)
        {
            InvalidOperationException.ThrowIfNull(_pieceGhostViewDefinitions);

            return Get(_pieceGhostViewDefinitions, pieceType);
        }

        [NotNull]
        private static IPieceViewDefinition Get(
            [NotNull] IEnumerable<PieceViewDefinition> pieceViewDefinitions,
            PieceType pieceType)
        {
            ArgumentNullException.ThrowIfNull(pieceViewDefinitions);

            foreach (PieceViewDefinition pieceViewDefinition in pieceViewDefinitions)
            {
                if (pieceViewDefinition?.PieceType != pieceType)
                {
                    continue;
                }

                return pieceViewDefinition;
            }

            InvalidOperationException.Throw($"Cannot get piece view definition with PieceType: {pieceType}");

            return null;
        }
    }
}