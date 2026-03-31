using UnityEngine;

namespace SaveLoadModule
{
    [System.Serializable]
    public class InventorySlotSave
    {
        [SerializeField]
        public string TabId;
        [SerializeField]
        public InventorySave[] ItemsData;
        [SerializeField]
        public int Capacity;
    }
}