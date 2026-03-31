using UnityEngine;
using UnityEngine.EventSystems;

namespace InventoryModule
{
    public class InventoryIconHoverableView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private InventoryIconViewModel _viewModel;
        
        [Zenject.Inject]
        private void Construct(InventoryIconViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _viewModel.OnPointerEnter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _viewModel.OnPointerExit();
        }
    }
}