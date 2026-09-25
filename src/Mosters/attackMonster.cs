using System;
using type_element;

public class AttackMonster
    {
        public string Name { get; }
        public int Damage { get; }
        public double ManaCost { get; }
        public TypeElement? Element { get; }
        public bool GrantsShield { get; }

        public AttackMonster(string name, int damage, double manaCost = 0, TypeElement? element = null, bool grantsShield = false)
        {
            Name = name;
            Damage = damage;
            ManaCost = manaCost;
            Element = element;
            GrantsShield = grantsShield;
        }

        public string GetDisplay()
        {
            string suffix = ManaCost > 0 ? $" (Mana: {ManaCost})" : "";
            return $"{Name}{suffix}";
        }
    }