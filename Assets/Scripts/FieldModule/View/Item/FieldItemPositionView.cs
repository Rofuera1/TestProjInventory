using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using R3;
using UnityEngine;

namespace FieldModule
{
    public class FieldItemPositionView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private Vector3 _refPosition;
        private Vector3 _prefferablePosition;
        
        private IEnumerator _dragCoroutine;
        private DisposableBag _disposableBag;

        public void Construct(FieldItemDragViewModel fieldItemDragViewModel)
        {
            fieldItemDragViewModel.StartDrag.Subscribe(StartSmoothDrag).AddTo(ref _disposableBag);
            fieldItemDragViewModel.DragPosition.Subscribe(SetSmoothPosition).AddTo(ref _disposableBag);
            fieldItemDragViewModel.EndDrag.Subscribe(EndSmoothDrag).AddTo(ref _disposableBag);
            
            fieldItemDragViewModel.LerpPosition.Subscribe(SetLerpPosition).AddTo(ref _disposableBag);
        }

        private void StartSmoothDrag(Unit _)
        {
            _spriteRenderer.sortingOrder = 2;
            _refPosition = Vector3.zero;
            _prefferablePosition = transform.position;
            
            if(_dragCoroutine != null)
                StopCoroutine(_dragCoroutine);

            StartCoroutine(_dragCoroutine = SmoothDrag());
        }
        
        private void SetSmoothPosition(Vector3 position) => _prefferablePosition = position;

        private void EndSmoothDrag(Unit _)
        {
            _spriteRenderer.sortingOrder = 1;// magic numbers i know
            
            if(_dragCoroutine != null)
                StopCoroutine(_dragCoroutine);
        }

        private void SetLerpPosition(Vector3 position) => transform.DOMove(position, .2f);

        private IEnumerator SmoothDrag()
        {
            var clamp = 0.2f; // TODO magic numbers

            while (true)
            {
                transform.position =
                    Vector3.SmoothDamp(transform.position, _prefferablePosition, ref _refPosition, clamp);
                
                yield return null;
            }
        }

        private void OnDestroy()
        {
            _disposableBag.Dispose();
        }
    }
}