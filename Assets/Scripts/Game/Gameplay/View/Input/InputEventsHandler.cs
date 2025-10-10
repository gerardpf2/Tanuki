using System;
using UnityEngine.EventSystems;

namespace Game.Gameplay.View.Input
{
    public class InputEventsHandler : IInputListener, IInputNotifier
    {
        public event Action<PointerEventData> OnBeginDrag;

        public event Action<PointerEventData> OnDrag;

        public event Action<PointerEventData> OnEndDrag;

        public void NotifyBeginDrag(PointerEventData eventData)
        {
            OnBeginDrag?.Invoke(eventData);
        }

        public void NotifyDrag(PointerEventData eventData)
        {
            OnDrag?.Invoke(eventData);
        }

        public void NotifyEndDrag(PointerEventData eventData)
        {
            OnEndDrag?.Invoke(eventData);
        }
    }
}