using System;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public class GameplayDefinition : IGameplayDefinition
    {
        [SerializeField] private string _id;
        [SerializeField] private string _data;

        public string Id => _id;

        public string Data => _data;
    }
}