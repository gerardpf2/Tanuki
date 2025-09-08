using Game.Gameplay.Board;
using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    [CreateAssetMenu(fileName = nameof(PieceViewDefinitionContainer), menuName = "Tanuki/Game/Gameplay/Board/" + nameof(PieceViewDefinitionContainer))]
    public class PieceViewDefinitionContainer : ScriptableObject, IPieceViewDefinitionGetter
    {
        [SerializeField] private PieceViewDefinition[] _pieceViewDefinitions;

        public IPieceViewDefinition Get(PieceType pieceType)
        {
            InvalidOperationException.ThrowIfNull(_pieceViewDefinitions);

            IPieceViewDefinition pieceViewDefinition = null;

            foreach (PieceViewDefinition pieceViewDefinitionCandidate in _pieceViewDefinitions)
            {
                InvalidOperationException.ThrowIfNull(pieceViewDefinitionCandidate);

                if (pieceViewDefinitionCandidate.PieceType != pieceType)
                {
                    continue;
                }

                pieceViewDefinition = pieceViewDefinitionCandidate;

                break;
            }

            InvalidOperationException.ThrowIfNullWithMessage(
                pieceViewDefinition,
                $"Cannot get piece view definition with PieceType: {pieceType}"
            );

            return pieceViewDefinition;
        }
    }
}