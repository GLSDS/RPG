using type_element;
using System;
using type_item;

namespace items
{
    public enum ItemType
    {
        Weapon,
        Armor,
        Consumable,
        Potion
    }
    public abstract class Items
    {
        public string ItemName { get; } = string.Empty;
        public ItemType ItemType { get; } = ItemType.Consumable;
        public int Quantity { get; set; } = 1;
        public string ItemDescription { get; } = string.Empty;
        public bool WayEffect { get; }
        public double HpEffect { get; }
        public TypeElement ItemElement { get; }

        private Items() { }

        protected Items(string itemName, ItemType itemType, int quantity, string itemDescription, bool wayEffect, double hpEffect, TypeElement itemElement = TypeElement.Normal)
        {
            ItemName = itemName;
            ItemType = itemType;
            Quantity = quantity;
            ItemDescription = itemDescription;
            WayEffect = wayEffect;
            HpEffect = hpEffect;
            ItemElement = itemElement;
        }
    }
    
}