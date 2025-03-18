using System;
using UnityEngine;

namespace Infrastructure.ScreenLoading
{
    [Serializable]
    public class ScreenDefinition : IScreenDefinition
    {
        [SerializeField] private string _key;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private string _placementKey;

        public string Key => _key;

        public GameObject Prefab => _prefab;

        public string PlacementKey => _placementKey;
    }
}