using UnityEngine;

namespace InventoryModule
{
    public abstract class BasicInventoryTabView : MonoBehaviour
    {
        [SerializeField] private GameObject _cellParent;
        public string Id { get; protected set; }

        public void SetActive(bool active)
        {
            _cellParent.SetActive(active);
        }
    }
}