using UnityEngine.EventSystems;

namespace Infrastructure.ModelViewViewModel.MethodBindings
{
    public class PointerUpBinding : MethodBinding, IPointerUpHandler
    {
        public void OnPointerUp(PointerEventData _)
        {
            Call();
        }
    }
}