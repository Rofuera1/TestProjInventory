using UnityEngine;

namespace InventoryModule
{
    [CreateAssetMenu(menuName = "Settings/TabSettings")]
    public class TabScriptable : ScriptableObject
    {
        public int Capacity;
        public AcceptanceType AcceptanceType;
        public ExtractionType ExtractionType;
    }
}