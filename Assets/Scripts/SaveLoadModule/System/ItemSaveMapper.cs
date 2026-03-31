using System;
using System.Linq;
using ItemModule;

namespace SaveLoadModule
{
    public class ItemSaveMapper : IItemSaveMapper
    {
        private readonly ItemFactory _itemFactory;

        public ItemSaveMapper(ItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
        }

        public ItemData ToData(IItem item)
        {
            return new ItemData
            {
                Type = item.Type,
                Properties = item.Properties.Select(ToData).ToArray()
            };
        }

        public IItem FromData(ItemData data)
        {
            var properties = data.Properties.Select(FromData).ToArray();
            return _itemFactory.Create(data.Type, properties);
        }

        private PropertyData ToData(IProperty property)
        {
            if (property is IPropertyN n)
            {
                return new PropertyData
                {
                    Type = PropertySaveType.N.ToString(),
                    IntValue = n.N
                };
            }

            return null;
        }

        private IProperty FromData(PropertyData data)
        {
            var parsed = Enum.TryParse<PropertySaveType>(data.Type, out var type);
            if(!parsed) throw new NotImplementedException();
                
            return type switch
            {
                PropertySaveType.N => new PropertyN(data.IntValue),
                _ => throw new NotSupportedException()
            };
        }
    }

}