using Infrastructure.System.Exceptions;
using Infrastructure.UnityUtils;
using UnityEngine;

namespace Infrastructure.Unity.MonoBehaviours
{
    public class CanvasSortingLayerNameSetter : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField, SortingLayerSelector] private string _sortingLayerName;

        private void Awake()
        {
            InvalidOperationException.ThrowIfNull(_canvas);

            _canvas.sortingLayerName = _sortingLayerName;
        }
    }
}