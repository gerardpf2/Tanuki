using System;
using UnityEngine;

namespace Infrastructure.ScreenLoading
{
    [Serializable]
    public class ScreenDefinition : IScreenDefinition
    {
        [SerializeField] private string _key;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private bool _usePlacement;
        [SerializeField] private string _placementKey; // TODO: Show / hide based on _usePlacement

        public string Key => _key;

        public GameObject Prefab => _prefab;

        public bool UsePlacement => _usePlacement;

        public string PlacementKey => _placementKey;
    }
}