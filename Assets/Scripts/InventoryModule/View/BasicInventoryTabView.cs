using UnityEngine;

namespace InventoryModule
{
    public abstract class BasicInventoryTabView : MonoBehaviour
    {
        [SerializeField] private GameObject _cellParent;
        [SerializeField] private string _id;

        public string Id => _id;

        public void SetActive(bool active)
        {
            _cellParent.SetActive(active);
        }
    }
}