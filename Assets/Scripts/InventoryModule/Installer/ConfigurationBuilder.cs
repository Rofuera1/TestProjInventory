using System;

namespace InventoryModule
{
    public static class ConfigurationBuilder
    {
        public static TabConfiguration Build(TabScriptable scriptable)
        {
            return new TabConfiguration(
                scriptable.Capacity, 
                scriptable.Id,
                MapAcceptanceRule(scriptable.AcceptanceType), 
                MapExtractionRule(scriptable.ExtractionType)
                );
        }

        public static IAcceptanceRule MapAcceptanceRule(AcceptanceType type)
        {
            return type switch
            {
                AcceptanceType.AZero => new AcceptanceAZeroRule(),
                AcceptanceType.APositive => new AcceptanceAPositive(),
                AcceptanceType.B => new AcceptanceBRule(),
                _ => throw new Exception($"Can't map Acceptance Rule to {type}")
            };
        }

        public static IExtractionRule MapExtractionRule(ExtractionType type)
        {
            return type switch
            {
                ExtractionType.Deny => new ExtractionDenyRule(),
                ExtractionType.Allow => new ExtractionAcceptRule(),
                _ => throw new Exception($"Can't map Extraction Rule to {type}")
            };
        }
    }
}