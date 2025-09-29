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

        public string Id => _id;

        public string Board => _board;

        public string Goals => _goals;
    }
}