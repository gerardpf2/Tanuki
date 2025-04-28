using System.Collections.Generic;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.Board
{
    [CreateAssetMenu(fileName = nameof(BoardDefinitionContainer), menuName = "Tanuki/Game/Gameplay/Board/" + nameof(BoardDefinitionContainer))]
    public class BoardDefinitionContainer : ScriptableObject, IBoardDefinitionGetter
    {
        [NotNull, ItemNotNull, SerializeField] private List<BoardDefinition> _boardDefinitions = new();

        public IBoardDefinition Get(string id)
        {
            IBoardDefinition boardDefinition = _boardDefinitions.Find(boardDefinition => boardDefinition.Id == id);

            InvalidOperationException.ThrowIfNullWithMessage(
                boardDefinition,
                $"Cannot get board definition with Id: {id}"
            );

            return boardDefinition;
        }
    }
}