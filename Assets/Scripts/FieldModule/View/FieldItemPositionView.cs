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
        private Vector3 _refPosition;
        private Vector3 _prefferablePosition;
        
        private IEnumerator _dragCoroutine;
        private DisposableBag _disposableBag;

        [Zenject.Inject]
        public void Construct(FieldItemViewModel fieldItemViewModel)
        {
            fieldItemViewModel.StartDrag.Subscribe(StartSmoothDrag).AddTo(ref _disposableBag);
            fieldItemViewModel.DragPosition.Subscribe(SetSmoothPosition).AddTo(ref _disposableBag);
            fieldItemViewModel.EndDrag.Subscribe(EndSmoothDrag).AddTo(ref _disposableBag);
            
            fieldItemViewModel.LerpPosition.Subscribe(SetLerpPosition).AddTo(ref _disposableBag);
        }

        private void StartSmoothDrag(Unit _)
        {
            _refPosition = Vector3.zero;
            _prefferablePosition = transform.position;
            
            if(_dragCoroutine != null)
                StopCoroutine(_dragCoroutine);

            StartCoroutine(_dragCoroutine = SmoothDrag());
        }
        
        private void SetSmoothPosition(Vector3 position) => _prefferablePosition = position;

        private void EndSmoothDrag(Unit _)
        {
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