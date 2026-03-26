using ItemModule;

namespace InventoryModule
{
    public interface IExtractionRule
    {
        public bool CanExtract(IItem item);
    }
}