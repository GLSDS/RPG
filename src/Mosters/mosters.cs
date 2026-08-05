using System;

namespace Mosters
{
    public class Mosters
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public string NameAttack1 { get; set; }
        public int DamageAttack1 { get; set; }
        public string NameAttack2 { get; set; }
        public int DamageAttack2 { get; set; }
        public bool Shield { get; set; }
        public int ProtectionOfShield { get; set; }

        public Mosters(string name, int health, string nameAttack1, int damageAttack1, string nameAttack2, int damageAttack2)
        {
            Name = name;
            Health = health;
            NameAttack1 = nameAttack1;
            DamageAttack1 = damageAttack1;
            NameAttack2 = nameAttack2;
            DamageAttack2 = damageAttack2;
            Shield = false;
            ProtectionOfShield = Health / 4;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Name: {Name}, Health: {Health}, Attack: {NameAttack1}: {DamageAttack1}, {NameAttack2}: {DamageAttack2}, Shield: {Shield}, Protection of Shield: {ProtectionOfShield}");
        }
    }
}