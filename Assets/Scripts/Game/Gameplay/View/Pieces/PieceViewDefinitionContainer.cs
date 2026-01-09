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
        [SerializeField] private PieceViewDefinition[] _boardPieceViewDefinitions;
        [SerializeField] private PieceViewDefinition[] _playerPieceViewDefinitions;
        [SerializeField] private PieceViewDefinition[] _playerPieceGhostViewDefinitions;

        public IPieceViewDefinition GetBoardPiece(PieceType pieceType)
        {
            InvalidOperationException.ThrowIfNull(_boardPieceViewDefinitions);

            return Get(_boardPieceViewDefinitions, pieceType);
        }

        public IPieceViewDefinition GetPlayerPiece(PieceType pieceType)
        {
            InvalidOperationException.ThrowIfNull(_playerPieceViewDefinitions);

            return Get(_playerPieceViewDefinitions, pieceType);
        }

        public IPieceViewDefinition GetPlayerPieceGhost(PieceType pieceType)
        {
            InvalidOperationException.ThrowIfNull(_playerPieceGhostViewDefinitions);

            return Get(_playerPieceGhostViewDefinitions, pieceType);
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