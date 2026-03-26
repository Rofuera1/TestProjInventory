using ItemModule;

namespace InventoryModule
{
    public class ExtractionAcceptRule : IExtractionRule
    {
        public bool CanExtract(IItem item) => true;
    }
}