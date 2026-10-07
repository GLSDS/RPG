using System;
using type_element;
using Monsters;
using Characters.Inventory;
using Characters;
using items;

namespace type_item
{
    public static class TypeItems
    {
        public class ironSword : Items
        {
            public ironSword() : base("Iron Sword", ItemType.Weapon, 1, "A basic iron sword.", false, 0, TypeElement.Fight)
            {
            }
        }

        public class healingPotion : Items
        {
            public healingPotion() : base("Healing Potion", ItemType.Potion, 1, "Restores 50 HP.", true, 50, TypeElement.Normal)
            {
            }
        }
    }
}