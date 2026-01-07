using UnityEngine;

namespace Infrastructure.ScreenLoading
{
    public class Screen : MonoBehaviour, IScreen
    {
        [SerializeField] private string _key;
        [SerializeField] private string _placementKey;
        [SerializeField] private bool _isolated;

        public string Key => _key;

        public string PlacementKey => _placementKey;

        public GameObject GameObject => gameObject;

        public bool Isolated => _isolated;

        public void OnFocus(bool focused)
        {
            GameObject.SetActive(focused);
        }
    }
}