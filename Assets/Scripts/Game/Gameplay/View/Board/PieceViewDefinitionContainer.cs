using System.Collections.Generic;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Board
{
    [CreateAssetMenu(fileName = nameof(PieceViewDefinitionContainer), menuName = "Tanuki/Game/Gameplay/Board/" + nameof(PieceViewDefinitionContainer))]
    public class PieceViewDefinitionContainer : ScriptableObject, IPieceViewDefinitionGetter
    {
        [NotNull, ItemNotNull, SerializeField] private List<PieceViewDefinition> _pieceViewDefinitions = new();

        public IPieceViewDefinition Get(PieceType pieceType)
        {
            IPieceViewDefinition pieceViewDefinition = _pieceViewDefinitions.Find(pieceViewDefinition => pieceViewDefinition.PieceType == pieceType);

            InvalidOperationException.ThrowIfNullWithMessage(
                pieceViewDefinition,
                $"Cannot get piece view definition with PieceType: {pieceType}"
            );

            return pieceViewDefinition;
        }
    }
}