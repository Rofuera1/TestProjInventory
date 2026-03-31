using UnityEngine;

namespace InventoryModule
{
    [CreateAssetMenu(menuName = "Settings/TabSettings")]
    public class TabScriptable : ScriptableObject
    {
        public int Capacity;
        public string Id;
        public AcceptanceType AcceptanceType;
        public ExtractionType ExtractionType;
    }
}