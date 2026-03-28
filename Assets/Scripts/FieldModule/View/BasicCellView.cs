using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FieldModule
{
    public class BasicCellView : MonoBehaviour, IBeginDragHandler
    {
        [SerializeField] private Image _itemSprite;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            
        }
    }
}