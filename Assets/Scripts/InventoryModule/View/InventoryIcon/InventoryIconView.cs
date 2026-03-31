using System;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventoryModule
{
    public class InventoryIconView : MonoBehaviour
    {
        [SerializeField] private Image _image;

        private DisposableBag _disposableBag;
        
        [Zenject.Inject]
        private void Construct(InventoryIconViewModel viewModel)
        {
            viewModel.IconColor.Subscribe(SetColor).AddTo(ref _disposableBag);
        }
        
        private void SetColor(Color color) => _image.color = color;

        private void OnDestroy()
        {
            _disposableBag.Dispose();
        }
    }
}