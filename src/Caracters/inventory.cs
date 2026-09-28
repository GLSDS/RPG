using system;
using type_element;
using List;

namespace inventory
{
    public class Item
    {
        public string ItemName { get; }
        public string ItemDescription { get; }
        public bool WayEffect { get; }
        public double HpEffect { get; }
        public TypeElement ItemElement{ get; }

        Item() { }

        public Item(string itemName, string itemDescription, bool wayEffect, double hpEffect, TypeElement itemElement = TypeElement.Normal)
        {
            ItemName = itemName;
            ItemDescription = itemDescription;
            WayEffect = wayEffect;
            HpEffect = hpEffect;
            ItemElement = itemElement;
        }
    }
}