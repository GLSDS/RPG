using System;
using System.Collections.Generic;
using System.Linq;
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
        public string Type { get; }
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

            double baseHp   = 40 + Level * 10;
            double baseMana = 20 + Level * 6;
            double baseXp   = 15 + Level * 8;

            MaxHp   = baseHp   + baseHp   * 0.4;
            MaxMana = baseMana + baseMana * 0.2;
            Xp      = baseXp   + baseXp   * 0.9;
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

        public List<Attack> Attacks { get; } = new List<Attack>();

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

    // ─────────────────────────────────────────────────────────────
    // FLOOR 1
    // ─────────────────────────────────────────────────────────────
    public class Slime : Monster
    {
        public Slime(ChoiceLevel choice = null)
            : base("Slime",
                   choice?.Level ?? 1,
                   choice?.MaxHp ?? 50,
                   choice?.MaxMana ?? 30,
                   choice?.Xp ?? 20,
                   type: "Beast")
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
                   choice?.Xp ?? 45,
                   type: "Undead")
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
                   choice?.MaxMana ?? 80,
                   choice?.Xp ?? 90,
                   type: "Undead")
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
                   choice?.Level ?? 1,
                   choice?.MaxHp ?? 90,
                   choice?.MaxMana ?? 50,
                   choice?.Xp ?? 120,
                   type: "Beast")
        {
            AddAttack("Bite", 45, 0);
            AddAttack("Web", 30, 27);
            AddAttack("Poison Attack", 48, 30);
        }
    }

    // ─────────────────────────────────────────────────────────────
    // FLOOR 2
    // ─────────────────────────────────────────────────────────────
    public class IceBear : Monster
    {
        public IceBear(ChoiceLevel choice = null)
            : base("Ice Bear",
                   choice?.Level ?? 1,
                   choice?.MaxHp ?? 120,
                   choice?.MaxMana ?? 60,
                   choice?.Xp ?? 150,
                   type: "Water")
        {
            AddAttack("Ice Punch", 50, 0);
            AddAttack("Frost Breath", 40, 20);
            AddAttack("Ice Shield", 0, 30, grantsShield: true);
        }
    }

    public class IceWizard : Monster
    {
        public IceWizard(ChoiceLevel choice = null)
            : base("Ice Wizard",
                   choice?.Level ?? 2,
                   choice?.MaxHp ?? 100,
                   choice?.MaxMana ?? 80,
                   choice?.Xp ?? 200,
                   type: "Water")
        {
            AddAttack("Frost fire", 60, 0);
            AddAttack("Snow Wool", 70, 25);
            AddAttack("Fireball", 50, 15);
        }
    }

    // ─────────────────────────────────────────────────────────────
    // MONSTER SPAWNER
    // ─────────────────────────────────────────────────────────────
    /// <summary>
    /// Fábrica de monstros: escolhe aleatoriamente um tipo de monstro
    /// e gera um level aleatório (1–10) com stats escalados.
    /// </summary>
    public static class MonsterSpawner
    {
        // Registro de todos os monstros disponíveis.
        // Para adicionar um novo monstro, basta incluir uma linha aqui.
        private static readonly Dictionary<string, Func<ChoiceLevel, Monster>> Registry = new()
        {
            { "Slime",           c => new Slime(c) },
            { "WarriorSkeleton", c => new WarriorSkeleton(c) },
            { "MageSkeleton",    c => new MageSkeleton(c) },
            { "GiantSpider",     c => new GiantSpider(c) },
            { "IceBear",         c => new IceBear(c) },
            { "IceWizard",       c => new IceWizard(c) },
        };

        /// <summary>
        /// Sorteia um monstro aleatório com level aleatório (1–10).
        /// </summary>
        public static Monster SpawnRandom()
        {
            var choice = new ChoiceLevel(); // level + stats aleatórios
            return CreateRandomMonster(choice);
        }

        /// <summary>
        /// Sorteia um monstro aleatório forçando um level específico (1–10).
        /// </summary>
        public static Monster SpawnRandom(int forcedLevel)
        {
            var choice = new ChoiceLevel(forcedLevel);
            return CreateRandomMonster(choice);
        }

        /// <summary>
        /// Cria um monstro aleatório a partir de um ChoiceLevel já pronto.
        /// </summary>
        private static Monster CreateRandomMonster(ChoiceLevel choice)
        {
            var keys = Registry.Keys.ToList();
            string chosen = keys[Random.Shared.Next(keys.Count)];
            return Registry[chosen](choice);
        }

        /// <summary>
        /// Mostra no console TODAS as características específicas de um monstro:
        /// stats base, estado atual (HP/Mana/Shield) e lista detalhada de ataques.
        /// </summary>
        public static void ShowMonsterDetails(Monster monster)
        {
            if (monster == null)
            {
                Console.WriteLine("Nenhum monstro para mostrar.");
                return;
            }

            Console.WriteLine("╔══════════════════════════════════════════════════╗");
            Console.WriteLine("║              MONSTRO GERADO                      ║");
            Console.WriteLine("╚══════════════════════════════════════════════════╝");

            Console.WriteLine($"  Nome   : {monster.Name}");
            Console.WriteLine($"  Level  : {monster.Level}");
            Console.WriteLine($"  Tipo   : {monster.Type}");
            Console.WriteLine($"  HP     : {monster.Hp} / {monster.MaxHp}");
            Console.WriteLine($"  Mana   : {monster.Mana} / {monster.MaxMana}");
            Console.WriteLine($"  XP     : {monster.Xp}");
            Console.WriteLine($"  Escudo : {(monster.Shield ? $"Ativo ({monster.ProtectionOfShield})" : "Inativo")}");

            Console.WriteLine();
            Console.WriteLine("  ───────────── ATAQUES ─────────────");
            if (monster.Attacks.Count == 0)
            {
                Console.WriteLine("  (nenhum ataque)");
            }
            else
            {
                for (int i = 0; i < monster.Attacks.Count; i++)
                {
                    var a = monster.Attacks[i];
                    string shieldTag = a.GrantsShield ? "  [ESCUDO]" : "";
                    string typeTag   = string.IsNullOrEmpty(a.Type) ? "" : $" ({a.Type})";

                    Console.WriteLine($"  {i + 1}. {a.Name}{typeTag}{shieldTag}");
                    Console.WriteLine($"     Dano: {a.Damage}   Mana: {a.ManaCost}");
                }
            }
            Console.WriteLine("  ───────────────────────────────────");
            Console.WriteLine();
        }

        /// <summary>
        /// Atalho: gera um monstro aleatório e já mostra tudo na tela.
        /// </summary>
        public static Monster SpawnAndShow()
        {
            Monster m = SpawnRandom();
            ShowMonsterDetails(m);
            return m;
        }

        /// <summary>
        /// Lista todos os monstros registrados (útil para debug).
        /// </summary>
        public static IEnumerable<string> ListMonsters() => Registry.Keys;
    }

    // ─────────────────────────────────────────────────────────────
    // PROGRAMA PRINCIPAL
    // ─────────────────────────────────────────────────────────────
    public class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Monstros disponíveis ===");
            foreach (var name in MonsterSpawner.ListMonsters())
                Console.WriteLine($"  - {name}");
            Console.WriteLine();

            // 1) Sorteia um monstro totalmente aleatório (tipo + level)
            Monster a = MonsterSpawner.SpawnAndShow();

            // 2) Sorteia outro, forçando level 5
            Monster b = MonsterSpawner.SpawnRandom(5);
            MonsterSpawner.ShowMonsterDetails(b);

            // 3) Batalha rápida entre os dois
            Console.WriteLine("=== BATALHA ===");
            int turno = 1;
            while (a.IsAlive() && b.IsAlive())
            {
                Console.WriteLine($"\n--- Turno {turno} ---");
                a.PerformRandomAttack(b);
                if (!b.IsAlive()) break;

                b.PerformRandomAttack(a);
                turno++;
            }

            Console.WriteLine();
            Console.WriteLine(a.IsAlive()
                ? $"🏆 {a.Name} venceu a batalha!"
                : $"🏆 {b.Name} venceu a batalha!");
        }
    }
}