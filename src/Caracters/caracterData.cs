using System;
using type_element;
using Characters.Inventory;

namespace Characters
{
    /// <summary>
    /// Concentra todos os dados base de um personagem.
    /// </summary>
    public class CharacterData
    {
        public string Name { get; set; } = "";
        public string ClassName { get; set; } = "Warrior";
        public String Representation { get; set; } = "⚔️";
        public int Level { get; set; } = 1;
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int Mana { get; set; }
        public int MaxMana { get; set; }
        public double Xp { get; set; }
        public double MaxXp { get; set; } = 100;
        public TypeElement Element { get; set; } = TypeElement.Normal;
        public InventarioSlots Inventory { get; set; } = new InventarioSlots(10);

        public CharacterData() { }

        public CharacterData(string name, string className, string representation, int level,
                             int hp, int maxHp, int mana, int maxMana,
                             int damage, double xp = 0, double maxXp = 100,
                             TypeElement element = TypeElement.Normal, InventarioSlots? inventory = null)
        {
            Name = name;
            ClassName = className;
            Representation = representation;
            Level = level;
            Hp = hp;
            MaxHp = maxHp;
            Mana = mana;
            MaxMana = maxMana;
            Xp = xp;
            MaxXp = maxXp;
            Element = element;
        }

        public CharacterData Clone() => (CharacterData)MemberwiseClone();
    }
}