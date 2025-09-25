using System;
using UnityEngine;

namespace Game.Gameplay.Goals
{
    [Serializable]
    public class GoalsDefinition : IGoalsDefinition
    {
        [SerializeField] private string _serializedData;

        public string SerializedData => _serializedData;
    }
}