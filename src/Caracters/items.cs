using type_element;
using System;

namespace items
{
    public class Items
    {
        public string ItemName { get; } = string.Empty;
        public string ItemDescription { get; } = string.Empty;
        public bool WayEffect { get; }
        public double HpEffect { get; }
        public TypeElement ItemElement { get; }

        private Items() { }

        public Items(string itemName, string itemDescription, bool wayEffect, double hpEffect, TypeElement itemElement = TypeElement.Normal)
        {
            ItemName = itemName;
            ItemDescription = itemDescription;
            WayEffect = wayEffect;
            HpEffect = hpEffect;
            ItemElement = itemElement;
        }
    }
}