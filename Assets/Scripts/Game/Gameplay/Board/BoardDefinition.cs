using System;
using UnityEngine;

namespace Game.Gameplay.Board
{
    [Serializable]
    public class BoardDefinition : IBoardDefinition
    {
        [SerializeField] private string _id;
        [SerializeField] private string _serializedData;

        public string Id => _id;

        public string SerializedData => _serializedData;
    }
}