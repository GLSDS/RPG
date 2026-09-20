
using System;
using System.Collections.Generic;
using System.Linq;

namespace Monsters
{
    public class Attack
    {
        public string Name { get; }
        public int Damage { get; }
        public double ManaCost { get; }
        public bool GrantsShield { get; }

        public Attack(string name, int damage, double manaCost = 0, bool grantsShield = false)
        {
            Name = name;
            Damage = damage;
            ManaCost = manaCost;
            GrantsShield = grantsShield;
        }

        public string GetDisplay()
        {
            string suffix = ManaCost > 0 ? $" (Mana: {ManaCost})" : "";
            return $"{Name}{suffix}";
        }
    }

    public class ChoiceLevel
    {
        private static readonly Random Rand = new Random();

        public int Level { get; }
        public double MaxHp { get; }
        public double MaxMana { get; }
        public double Xp { get; }

        public ChoiceLevel()
        {
            Level = Rand.Next(1, 11);   
            MaxHp = Level * 0.4;         
            MaxMana = Level * 0.2;       
            Xp = Level * 0.9;            
        }
    }

    // ─────────────────────────────────────────────────────────────
    // Monster (nome no singular): classe-base dos inimigos.
    // Um único fluxo de ataque, com alvo, mana e escudo resolvidos aqui.
    // ─────────────────────────────────────────────────────────────
    public abstract class Monster
    {
        private static readonly Random Rand = new Random();

        public string Name { get; protected set; }
        public int Level { get; private set; }
        public double Hp { get; private set; }
        public double MaxHp { get; private set; }
        public double Mana { get; private set; }
        public double MaxMana { get; private set; }
        public double Xp { get; set; }

        // Somente leitura para fora: ninguém pode substituir a lista inteira.
        public List<Attack> Attacks { get; } = new List<Attack>();

        public bool Shield { get; private set; }
        public int ProtectionOfShield { get; private set; }

        // Um só construtor: o monstro nasce com HP/mana cheios.
        // ProtectionOfShield é calculado sobre MaxHp (não sobre um hp já ferido).
        protected Monster(string name, int level, double maxHp, double maxMana,
                          double xp = 0, bool shield = false)
        {
            Name = name;
            Level = level;
            MaxHp = maxHp;
            Hp = maxHp;
            MaxMana = maxMana;
            Mana = maxMana;
            Xp = xp;
            Shield = shield;
            ProtectionOfShield = (int)(maxHp / 4);
        }

        public void AddAttack(string name, int damage, int manaCost = 0, bool grantsShield = false)
        {
            Attacks.Add(new Attack(name, damage, manaCost, grantsShield));
        }

        private bool CanAfford(Attack attack) => attack.ManaCost <= Mana;

        // ── Fluxo único de uso de ataque (resolve o código duplicado) ──
        private bool TryUseAttack(Attack attack, Monster target)
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

        public void ChooseAttack(int index, Monster target = null)
        {
            if (index < 0 || index >= Attacks.Count)
            {
                Console.WriteLine("Invalid attack choice.");
                return;
            }
            TryUseAttack(Attacks[index], target);
        }

        public void ChooseAttack(string attackName, Monster target = null)
        {
            Attack found = Attacks.FirstOrDefault(a =>
                string.Equals(a.Name, attackName, StringComparison.OrdinalIgnoreCase));

            if (found == null)
            {
                Console.WriteLine($"Attack '{attackName}' not found!");
                return;
            }

            TryUseAttack(found, target);
        }

        public void ShowAndChooseAttack(Monster target = null)
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
            Console.WriteLine($"HP: {Hp}/{MaxHp}");
            Console.WriteLine($"XP: {Xp}");
            Console.WriteLine($"Mana: {Mana}/{MaxMana}");
            Console.WriteLine($"Shield: {(Shield ? "Active" : "Inactive")}");
            Console.WriteLine($"Shield Protection: {ProtectionOfShield}");
        }

        public virtual void ShowAttacks()
        {
            Console.WriteLine($"\n=== {Name}'s Attacks ===");
            for (int i = 0; i < Attacks.Count; i++)
                Console.WriteLine($"{i + 1}. {Attacks[i].GetDisplay()}");
        }

        // ── Ataque aleatório: valida lista vazia, filtra por mana e aplica dano ──
        public virtual void PerformRandomAttack(Monster target = null)
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

            Attack chosen = affordable[Rand.Next(affordable.Count)];
            TryUseAttack(chosen, target);
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0) damage = 0;

            // O escudo consome apenas o dano que realmente bloqueia.
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

    /****************************************************FLOOR 1****************************************************/

    public class Slime : Monster
    {
        
        
        public Slime(ChoiceLevel choice = null)
            : base("Slime",
                   choice?.Level ?? 1,
                   choice?.MaxHp ?? 50,
                   choice?.MaxMana ?? 30,
                   choice?.Xp ?? 20)
        {
            AddAttack("Normal Attack", 20, 0);
            AddAttack("Slime Splash", 10, 5);
            AddAttack("Bouncy", 15, 10);
            AddAttack("Poison Spit", 25, 20);
        }
    }

    public class WarriorSkeleton : Monster
    {
        public WarriorSkeleton(ChoiceLevel choice = null) 
        : base("WarriorSkeleton",
        choice?.Level ?? 1,
        choice?.MaxHp ?? 70,
        choice?.MaxMana ?? 50,
        choice?.Xp ?? 45)
        {
            AddAttack("Normal Attack", 28, 0);
            AddAttack("Slash", 30, 0);
            AddAttack("Shield Bash Combo", 30, 0);
        }
    }

    public class MageSkeleton : Monster
    {
        public MageSkeleton(ChoiceLevel choice = null)
         : base("MageSkeleton",
         choice?.Level ?? 1,
         choice?.MaxHp ?? 60,
         choice?.MaxHp ?? 80,
         choice?.Xp ?? 90)
        {
            AddAttack("Fire Ball", 28, 0);
            AddAttack("Slash", 30, 0);
            AddAttack("Shield Bash Combo", 30, 0);
        }
    }

    public class GiantSpider : Monster
    {
        public GiantSpider(ChoiceLevel choice = null)
        : base("Giant Spider",
        choice?.Level?? 1,
        choice?.MaxHp ?? 90,
        choice?.MaxHp ?? 50,
        choice?.Xp ?? 120)
        {
            AddAttack("Bite", 45, 0);
            AddAttack("Web", 30, 27);
            AddAttack("Poison Attack", 48, 30);
        }
    }

    /****************************************************FLOOR 2****************************************************/

    public class IceBear : Monster        // PascalCase
    {
        public IceBear(ChoiceLevel choice = null)
        : base("Ice Bear",
        choice?.Level ?? 1,
        choice?.MaxHp ?? 120,
        choice?.MaxMana ?? 60,
        choice?.Xp ?? 150)
        {
            AddAttack("Ice Punch", 50, 0);
            AddAttack("Frost Breath", 40, 20);
            AddAttack("Ice Shield", 0, 30, grantsShield: true);  // agora tem efeito real
        }
    }

    public class IceWizard : Monster
    {
        public IceWizard(ChoiceLevel choice = null)
         : base("Ice Wizard",
         choice?.Level ?? 2,
         choice?.MaxHp ?? 100,
         choice?.MaxMana ?? 80,
         choice?.Xp ?? 200)
        {
            AddAttack("Frost fire", 60, 0);
            AddAttack("Inferno", 70, 25);
            AddAttack("Fireball", 50, 15);
        }
    }
}
