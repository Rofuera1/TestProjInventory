namespace InventoryModule
{
    public interface IExtractionSystem
    {
        public void TryExtractItem(int itemPosition, IInventoryTab tab);
    }
}