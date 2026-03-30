using System;
using UnityEngine;
using UnityEngine.UI;

namespace FieldModule
{
    public class CreateRandomItemButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        [Zenject.Inject] private IFieldItemSystem _fieldSystem;

        private void Awake() => _button.onClick.AddListener(_fieldSystem.CreateRandomItem);

        private void OnValidate() => _button = _button ?? GetComponent<Button>();
    }
}