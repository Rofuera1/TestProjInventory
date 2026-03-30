using System;
using R3;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FieldModule
{
    public class FieldItemView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _image;

        private DisposableBag _disposableBag;
        
        public void Construct(FieldItemViewModel fieldItemViewModel)
        {
            fieldItemViewModel.Icon.Subscribe(SetIcon).AddTo(ref _disposableBag);
        }
        
        private void SetIcon(Sprite sprite) => _image.sprite = sprite;

        private void OnDestroy()
        {
            _disposableBag.Dispose();
        }

        private void OnValidate() => _image = _image ?? GetComponent<SpriteRenderer>();
    }
}