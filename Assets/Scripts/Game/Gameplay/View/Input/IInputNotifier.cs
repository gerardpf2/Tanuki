using UnityEngine.EventSystems;

namespace Game.Gameplay.View.Input
{
    public interface IInputNotifier
    {
        void NotifyBeginDrag(PointerEventData eventData);

        void NotifyDrag(PointerEventData eventData);

        void NotifyEndDrag(PointerEventData eventData);

        void NotifyPointerClick(PointerEventData eventData);
    }
}