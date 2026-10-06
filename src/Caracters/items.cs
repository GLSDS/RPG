using type_element;
using System;

namespace items
{
    public class Items
    {
        public string ItemName { get; } = string.Empty;
        public int Quantity { get; set; } = 1;
        public string ItemDescription { get; } = string.Empty;
        public bool WayEffect { get; }
        public double HpEffect { get; }
        public TypeElement ItemElement { get; }

        private Items() { }

        public Items(string itemName, int quantity, string itemDescription, bool wayEffect, double hpEffect, TypeElement itemElement = TypeElement.Normal)
        {
            ItemName = itemName;
            Quantity = quantity;
            ItemDescription = itemDescription;
            WayEffect = wayEffect;
            HpEffect = hpEffect;
            ItemElement = itemElement;
        }
    }
}