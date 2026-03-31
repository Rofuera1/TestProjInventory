using UnityEngine;
using UnityEngine.UI;

namespace SaveLoadModule
{
    public class SaveButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [Zenject.Inject] private ISaveSystem _saveSystem;

        private void Awake() => _button.onClick.AddListener(() => _saveSystem.Save());

        private void OnValidate() => _button = _button ?? GetComponent<Button>();
    }
}