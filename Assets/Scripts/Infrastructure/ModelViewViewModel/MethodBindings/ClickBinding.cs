using UnityEngine.EventSystems;

namespace Infrastructure.ModelViewViewModel.MethodBindings
{
    public class ClickBinding : MethodBinding, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData _)
        {
            Call();
        }
    }
}