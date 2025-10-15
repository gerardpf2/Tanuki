using System;
using UnityEngine.EventSystems;

namespace Game.Gameplay.View.Input
{
    public interface IInputListener
    {
        event Action<PointerEventData> OnBeginDrag;

        event Action<PointerEventData> OnDrag;

        event Action<PointerEventData> OnEndDrag;

        event Action<PointerEventData> OnPointerClick;
    }
}