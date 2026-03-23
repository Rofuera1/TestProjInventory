using R3;
using UnityEngine;
using UnityEngine.UI;

namespace ItemModule
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private Image _itemImage;
        
        private DisposableBag _bag;
        
        [Zenject.Inject]
        public void Construct(ItemViewModel viewModel)
        {
            viewModel.ItemImage.Subscribe(UpdateImage).AddTo(ref _bag);
            viewModel.ItemVisible.Subscribe(UpdateVisibility).AddTo(ref _bag);
        }

        private void UpdateImage(Sprite image) => _itemImage.sprite = image;
        
        private void UpdateVisibility(bool visible) => _itemImage.enabled = visible;
    }
}