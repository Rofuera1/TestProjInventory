using UnityEngine;

namespace InventoryModule
{
    public abstract class BasicInventoryTabView : MonoBehaviour
    {
        [SerializeField] private GameObject _cellParent;
        [SerializeField] private TabScriptable _params;

        public string Id => _params.Id;

        public void SetActive(bool active)
        {
            _cellParent.SetActive(active);
        }
    }
}