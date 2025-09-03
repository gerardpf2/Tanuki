using System;
using Game.Gameplay.Board;
using UnityEngine;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay
{
    [Serializable]
    public class GameplayDefinition : IGameplayDefinition
    {
        [SerializeField] private string _id;
        [SerializeField] private BoardDefinition _boardDefinition;

        public string Id => _id;

        public IBoardDefinition BoardDefinition
        {
            get
            {
                InvalidOperationException.ThrowIfNull(_boardDefinition);

                return _boardDefinition;
            }
        }
    }
}