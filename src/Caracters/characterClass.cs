using System;
using System.Collections.Generic;
using System.Linq;
using type_element;

namespace Characters
{
    public abstract class CharacterClass
    {
        public CharacterData Data { get; }
        public List<AttackCharacter> Attacks { get; } = new();

        public string Name => Data.Name;
        public int Level => Data.Level;
        public int Hp { get => Data.Hp; private set => Data.Hp = value; }
        public int MaxHp => Data.MaxHp;
        public int Mana { get => Data.Mana; private set => Data.Mana = value; }
        public int MaxMana => Data.MaxMana;
        public int Damage => Data.Damage;
        public double Xp { get => Data.Xp; private set => Data.Xp = value; }
        public double MaxXp => Data.MaxXp;
        public TypeElement Element => Data.Element;
        public bool IsAlive => Hp > 0;
        public bool Shield { get; private set; }
        private int ShieldProtection { get; }

        protected CharacterClass(CharacterData data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
            ShieldProtection = Math.Max(1, Data.MaxHp / 4);
            RegisterAttacks();
        }

        protected abstract void RegisterAttacks();

        protected void AddAttack(string name, int damage, double manaCost = 0,
                                 TypeElement? element = null, bool grantsShield = false)
        {
            Attacks.Add(new AttackCharacter(name, damage, manaCost, element, grantsShield));
        }

        public void Attack(CharacterClass target, string attackName)
        {
            if (target == null || !target.IsAlive)
                return;

            if (string.Equals(attackName, "Ataque Básico", StringComparison.OrdinalIgnoreCase))
            {
                target.TakeDamage(Damage);
                return;
            }

            var attack = FindAttack(attackName);
            if (attack == null || !CanUse(attack))
                return;

            Mana -= (int)attack.ManaCost;
            if (attack.GrantsShield)
            {
                Shield = true;
                Console.WriteLine($"{Name} ativou um escudo ({ShieldProtection} de proteção).");
                return;
            }

            target.TakeDamage(ApplyEffectiveness(attack, target.Element, attack.Damage));
        }

        public void Attack(Monsters.Monster target, string attackName)
        {
            if (target == null || !target.IsAlive())
                return;

            if (string.Equals(attackName, "Ataque Básico", StringComparison.OrdinalIgnoreCase))
            {
                target.TakeDamage(Damage);
                return;
            }

            var attack = FindAttack(attackName);
            if (attack == null || !CanUse(attack))
                return;

            Mana -= (int)attack.ManaCost;
            if (attack.GrantsShield)
            {
                Shield = true;
                Console.WriteLine($"{Name} ativou um escudo ({ShieldProtection} de proteção).");
                return;
            }

            target.TakeDamage(ApplyEffectiveness(attack, target.Element, attack.Damage));
        }

        public void Heal(int amount)
        {
            if (amount <= 0 || !IsAlive)
                return;

            int restored = Math.Min(amount, MaxHp - Hp);
            Hp += restored;
            Console.WriteLine($"{Name} recuperou {restored} HP ({Hp}/{MaxHp}).");
        }

        public void TakeDamage(int amount)
        {
            amount = Math.Max(0, amount);
            if (Shield && amount > 0)
            {
                int blocked = Math.Min(amount, ShieldProtection);
                amount -= blocked;
                Shield = false;
                Console.WriteLine($"{Name} bloqueou {blocked} de dano com o escudo.");
            }

            Hp = Math.Max(0, Hp - amount);
            Console.WriteLine($"{Name} recebeu {amount} de dano. HP: {Hp}/{MaxHp}.");
        }

        public void GainXp(int amount)
        {
            if (amount <= 0)
                return;

            Xp += amount;
            Console.WriteLine($"{Name} ganhou {amount} XP. Total: {Xp}/{MaxXp}.");
        }

        public void DisplayStats()
        {
            Console.WriteLine($"=== {Name} ===");
            Console.WriteLine($"Classe: {Data.ClassName}");
            Console.WriteLine($"Nível: {Level}");
            Console.WriteLine($"HP: {Hp}/{MaxHp}");
            Console.WriteLine($"Mana: {Mana}/{MaxMana}");
            Console.WriteLine($"Dano básico: {Damage}");
            Console.WriteLine($"XP: {Xp}/{MaxXp}");
        }

        private AttackCharacter? FindAttack(string attackName)
        {
            var attack = Attacks.FirstOrDefault(candidate =>
                string.Equals(candidate.Name, attackName, StringComparison.OrdinalIgnoreCase));

            if (attack == null)
                Console.WriteLine($"Ataque '{attackName}' não encontrado.");

            return attack;
        }

        private bool CanUse(AttackCharacter attack)
        {
            if (Mana >= attack.ManaCost)
                return true;

            Console.WriteLine($"Mana insuficiente para usar {attack.Name}.");
            return false;
        }

        private int ApplyEffectiveness(AttackCharacter attack, TypeElement defense, int baseDamage)
        {
            double multiplier = new ElementTypeEffectiveness().GetEffectiveness(attack.Element ?? Element, defense);
            return (int)Math.Ceiling(baseDamage * multiplier);
        }
    }
}