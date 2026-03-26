namespace InventoryModule
{
    public class TabConfiguration
    {
        public int Capacity { get; }
        
        public IAcceptanceRule AcceptanceRule { get; }
        public IExtractionRule ExtractionRule { get; }
        
        public TabConfiguration(
            int capacity,
            IAcceptanceRule acceptanceRule,
            IExtractionRule extractionRule)
        {
            Capacity = capacity;
            AcceptanceRule = acceptanceRule;
            ExtractionRule = extractionRule;
        }
    }
}