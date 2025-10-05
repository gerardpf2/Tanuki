using System;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public class GameplayDefinition : IGameplayDefinition
    {
        [SerializeField] private string _id;
        [SerializeField] private string _board;
        [SerializeField] private string _goals;
        [SerializeField] private string _data;

        public string Id => _id;

        public string Board => _board;

        public string Goals => _goals;

        public string Data => _data;
    }
}