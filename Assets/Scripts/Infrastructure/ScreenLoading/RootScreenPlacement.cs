using UnityEngine;

namespace Infrastructure.ScreenLoading
{
    public class RootScreenPlacement : MonoBehaviour, IScreenPlacement
    {
        public string Key => "Root";

        public Transform Transform => transform;
    }
}