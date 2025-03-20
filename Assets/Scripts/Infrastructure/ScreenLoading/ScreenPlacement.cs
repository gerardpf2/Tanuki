using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
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

            InvalidOperationException.ThrowIfNull(_screenPlacementAdder);

            _screenPlacementAdder.Add(this);
        }

        private void OnDestroy()
        {
            InvalidOperationException.ThrowIfNull(_screenPlacementAdder);

            _screenPlacementAdder.Remove(this);
        }

        public void Inject([NotNull] IScreenPlacementAdder screenPlacementAdder)
        {
            ArgumentNullException.ThrowIfNull(screenPlacementAdder);

            _screenPlacementAdder = screenPlacementAdder;
        }
    }
}