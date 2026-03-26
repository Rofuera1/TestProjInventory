using ItemModule;

namespace InventoryModule
{
    public class ExtractionDenyRule : IExtractionRule
    {
        public bool CanExtract(IItem item) => false;
    }
}