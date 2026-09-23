using System;
using System.Collections.Generic;
using floor1;
using floor2;
using type_element;
using Random = System.Random;





namespace Monsters
{
    // ─────────────────────────────────────────────────────────────
    // ATTACK
    // ─────────────────────────────────────────────────────────────
    public class Attack
    {
        public string Name { get; }
        public int Damage { get; }
        public double ManaCost { get; }
        public string Type { get; private set; }
        public bool GrantsShield { get; }

        public Attack(string name, int damage, double manaCost = 0, string type = "", bool grantsShield = false)
        {
            Name = name;
            Damage = damage;
            ManaCost = manaCost;
            Type = type;
            GrantsShield = grantsShield;
        }

        public string GetDisplay()
        {
            string suffix = ManaCost > 0 ? $" (Mana: {ManaCost})" : "";
            return $"{Name}{suffix}";
        }
    }

    // ─────────────────────────────────────────────────────────────
    // CHOICE LEVEL
    // ─────────────────────────────────────────────────────────────
    public class ChoiceLevel
    {
        public int Level { get; }
        public double MaxHp { get; }
        public double MaxMana { get; }
        public double Xp { get; }

        // Construtor sem parâmetros: gera level aleatório E calcula os stats.
        public ChoiceLevel() : this(Random.Shared.Next(1, 11)) { }

        // Construtor por level: calcula os stats com base na fórmula.
        public ChoiceLevel(int level)
        {
            if (level < 1) level = 1;
            if (level > 10) level = 10;

            Level = level;

            double baseHp = 40 + Level * 10;
            double baseMana = 20 + Level * 6;
            double baseXp = 15 + Level * 8;

            MaxHp = baseHp + baseHp * 0.4;
            MaxMana = baseMana + baseMana * 0.2;
            Xp = baseXp + baseXp * 0.9;
        }

        // Construtor completo (mantido para compatibilidade).
        public ChoiceLevel(int level, double maxHp, double maxMana, double xp)
        {
            Level = level;
            MaxHp = maxHp;
            MaxMana = maxMana;
            Xp = xp;
        }
    }

    // ─────────────────────────────────────────────────────────────
    // MONSTER (base)
    // ─────────────────────────────────────────────────────────────
    public abstract class Monster
    {
        public string Name { get; protected set; }
        public int Level { get; private set; }
        public double Hp { get; private set; }
        public double MaxHp { get; private set; }
        public double Mana { get; private set; }
        public double MaxMana { get; private set; }
        public string Type { get; protected set; }
        public double Xp { get; set; }

        public List<Attack> Attacks { get; } = [];

        public bool Shield { get; private set; }
        public int ProtectionOfShield { get; private set; }

        protected Monster(string name, int level, double maxHp, double maxMana,
                          double xp = 0, string type = "", bool shield = false)
        {
            Name = name;
            Level = level;
            MaxHp = maxHp;
            Hp = maxHp;
            MaxMana = maxMana;
            Mana = maxMana;
            Xp = xp;
            Type = type;
            Shield = shield;
            ProtectionOfShield = (int)(maxHp / 4);
        }

        public void AddAttack(string name, int damage, double manaCost = 0, string type = "", bool grantsShield = false)
        {
            Attacks.Add(new Attack(name, damage, manaCost, type, grantsShield));
        }

        private bool CanAfford(Attack attack) => attack.ManaCost <= Mana;

        private bool TryUseAttack(Attack? attack, Monster? target)
        {
            if (attack == null)
                return false;

            if (!CanAfford(attack))
            {
                Console.WriteLine($"Not enough mana! Need {attack.ManaCost}, have {Mana}");
                return false;
            }

            Mana -= attack.ManaCost;
            Console.WriteLine($"{Name} used {attack.Name}!");

            if (attack.GrantsShield)
            {
                Shield = true;
                Console.WriteLine($"{Name} raised a shield! ({ProtectionOfShield} protection)");
                return true;
            }

            if (target != null && attack.Damage > 0)
                target.TakeDamage(attack.Damage);
            else
                Console.WriteLine($"Dealt {attack.Damage} damage!");

            return true;
        }

        public void ChooseAttack(int index, Monster? target = null)
        {
            if (index < 0 || index >= Attacks.Count)
            {
                Console.WriteLine("Invalid attack choice.");
                return;
            }
            TryUseAttack(Attacks[index], target);
        }

        public void ChooseAttack(string attackName, Monster? target = null)
        {
            Attack? found = Attacks.FirstOrDefault(a =>
                string.Equals(a.Name, attackName, StringComparison.OrdinalIgnoreCase));

            if (found == null)
            {
                Console.WriteLine($"Attack '{attackName}' not found!");
                return;
            }

            TryUseAttack(found, target);
        }

        public void ShowAndChooseAttack(Monster? target = null)
        {
            ShowAttacks();
            Console.Write("\nChoose an attack (number): ");

            if (int.TryParse(Console.ReadLine(), out int choice))
                ChooseAttack(choice - 1, target);
            else
                Console.WriteLine("Invalid input!");
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"=== {Name} ===");
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"Type: {Type}");
            Console.WriteLine($"HP: {Hp}/{MaxHp}");
            Console.WriteLine($"Mana: {Mana}/{MaxMana}");
            Console.WriteLine($"XP: {Xp}");
        }

        public virtual void ShowAttacks()
        {
            Console.WriteLine($"\n=== {Name}'s Attacks ===");
            for (int i = 0; i < Attacks.Count; i++)
                Console.WriteLine($"{i + 1}. {Attacks[i].GetDisplay()}");
        }

        public virtual void PerformRandomAttack(Monster? target = null)
        {
            Console.WriteLine($"\n=== {Name} is attacking! ===");

            if (Attacks.Count == 0)
            {
                Console.WriteLine($"{Name} has no attacks and skips the turn!");
                return;
            }

            var affordable = Attacks.Where(CanAfford).ToList();
            if (affordable.Count == 0)
            {
                Console.WriteLine($"{Name} has no mana for any attack and skips the turn!");
                return;
            }

            Attack chosen = affordable[Random.Shared.Next(affordable.Count)];
            TryUseAttack(chosen, target);
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0) damage = 0;

            if (Shield && damage > 0)
            {
                int blocked = Math.Min(damage, ProtectionOfShield);
                damage -= blocked;
                Shield = false;
                Console.WriteLine($"{Name} blocked {blocked} damage with shield! (Shield consumed)");
            }

            Hp = Math.Max(0, Hp - damage);
            Console.WriteLine($"{Name} took {damage} damage! HP: {Hp}/{MaxHp}");

            if (!IsAlive())
                Console.WriteLine($"{Name} has been defeated!");
        }

        public bool IsAlive() => Hp > 0;

        public void RestoreMana(int amount)
        {
            double before = Mana;
            Mana = Math.Min(MaxMana, Mana + amount);
            Console.WriteLine($"{Name} restored {Mana - before} mana! Mana: {Mana}/{MaxMana}");
        }
    }
}