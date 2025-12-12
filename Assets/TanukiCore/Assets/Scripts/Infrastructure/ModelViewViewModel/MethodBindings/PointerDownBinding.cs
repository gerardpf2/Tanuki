using UnityEngine.EventSystems;

namespace Infrastructure.ModelViewViewModel.MethodBindings
{
    public class PointerDownBinding : MethodBinding, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData _)
        {
            Call();
        }
    }
}