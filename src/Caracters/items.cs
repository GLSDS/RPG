using type_element;
using System;

namespace items
{
    public class Item
    {
        public string ItemName { get; }
        public string ItemDescription { get; }
        public bool WayEffect { get; }
        public double HpEffect { get; }
        public TypeElement ItemElement { get; }

        private Item() { }

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