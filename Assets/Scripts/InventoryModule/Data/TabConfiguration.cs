namespace InventoryModule
{
    public class TabConfiguration
    {
        public int Capacity { get; }
        public string Id { get; }
        public IAcceptanceRule AcceptanceRule { get; }
        public IExtractionRule ExtractionRule { get; }
        
        public TabConfiguration(
            int capacity,
            string id,
            IAcceptanceRule acceptanceRule,
            IExtractionRule extractionRule)
        {
            Capacity = capacity;
            Id = id;
            AcceptanceRule = acceptanceRule;
            ExtractionRule = extractionRule;
        }
    }
}