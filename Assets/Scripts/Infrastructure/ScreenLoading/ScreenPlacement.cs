using Infrastructure.DependencyInjection;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.ScreenLoading
{
    public class ScreenPlacement : MonoBehaviour, IScreenPlacement
    {
        [SerializeField] private string _key;

        public string Key => _key;

        public Transform Transform => transform;

        private IScreenPlacementAdder _screenPlacementAdder;

        private void Awake()
        {
            InjectResolver.Resolve(this);

            _screenPlacementAdder.Add(this);
        }

        private void OnDestroy()
        {
            _screenPlacementAdder.Remove(this);
        }

        public void Inject([NotNull] IScreenPlacementAdder screenPlacementAdder)
        {
            _screenPlacementAdder = screenPlacementAdder;
        }
    }
}