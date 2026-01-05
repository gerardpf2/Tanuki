using UnityEngine;

namespace Infrastructure.ScreenLoading
{
    public class Screen : MonoBehaviour, IScreen
    {
        [SerializeField] private string _placementKey;

        public string PlacementKey => _placementKey;

        public GameObject GameObject => gameObject;
    }
}