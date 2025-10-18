using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Gameplay.View.Input
{
    public class InputCatcher : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        private IInputNotifier _inputNotifier;

        private void Awake()
        {
            InjectResolver.Resolve(this);
        }

        public void Inject([NotNull] IInputNotifier inputNotifier)
        {
            ArgumentNullException.ThrowIfNull(inputNotifier);

            _inputNotifier = inputNotifier;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            InvalidOperationException.ThrowIfNull(_inputNotifier);

            _inputNotifier.NotifyBeginDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            InvalidOperationException.ThrowIfNull(_inputNotifier);

            _inputNotifier.NotifyDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            InvalidOperationException.ThrowIfNull(_inputNotifier);

            _inputNotifier.NotifyEndDrag(eventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            InvalidOperationException.ThrowIfNull(_inputNotifier);

            _inputNotifier.NotifyPointerClick(eventData);
        }
    }
}