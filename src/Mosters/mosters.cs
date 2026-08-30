using System;
using System.Collections.Generic;

namespace Monsters
{
    public class Attack
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public int ManaCost { get; set; }

        public Attack(string name, int damage, int manaCost = 0)
        {
            Name = name;
            Damage = damage;
            ManaCost = manaCost;
        }

        public string GetDisplay()
        {
            return $"{Name} {(ManaCost > 0 ? $"(Mana: {ManaCost})" : "")}";
        }
    }

    public class Monsters
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int Mana { get; set; }
        public int MaxMana { get; set; }
        public int Xp { get; set; }
        public List<Attack> Attacks { get; set; }
        public bool Shield { get; set; }
        public int ProtectionOfShield { get; set; }

        public Monsters(string name, int level, int hp, int mana, int xp = 0)
        {
            Name = name;
            Level = level;
            Hp = hp;
            MaxHp = hp;
            Mana = mana;
            MaxMana = mana;
            Xp = xp;
            Attacks = new List<Attack>();
            Shield = false;
            ProtectionOfShield = hp / 4;
        }

        public void AddAttack(string name, int damage, int manaCost = 0)
        {
            Attacks.Add(new Attack(name, damage, manaCost));
        }

        // Método corrigido para escolher ataque por índice
        public void ChooseAttack(int index)
        {
            if (index < 0 || index >= Attacks.Count)
            {
                Console.WriteLine("Invalid attack choice.");
                return;
            }

            Attack chosenAttack = Attacks[index];
            
            // Verifica se tem mana suficiente
            if (chosenAttack.ManaCost > Mana)
            {
                Console.WriteLine($"Not enough mana! Need {chosenAttack.ManaCost}, have {Mana}");
                return;
            }

            Mana -= chosenAttack.ManaCost;
            Console.WriteLine($"{Name} used {chosenAttack.Name}!");
            Console.WriteLine($"Dealt {chosenAttack.Damage} damage!");
        }

        // Método para escolher ataque por nome
        #pragma warning disable CS8600
        public void ChooseAttack(string attackName)
        {
            Attack foundAttack = null;
            foreach (var a in Attacks)
            {
                if (string.Equals(a.Name ?? "", attackName, StringComparison.OrdinalIgnoreCase))
                {
                    foundAttack = a;
                    break;
                }
            }

            if (foundAttack == null)
            {
                Console.WriteLine($"Attack '{attackName}' not found!");
                return;
            }

            if (foundAttack.ManaCost > Mana)
            {
                Console.WriteLine($"Not enough mana! Need {foundAttack.ManaCost}, have {Mana}");
                return;
            }

            Mana -= foundAttack.ManaCost;
            Console.WriteLine($"{Name} used {foundAttack.Name}!");
            Console.WriteLine($"Dealt {foundAttack.Damage} damage!");
        }
        #pragma warning restore CS8600

        // Método para mostrar ataques e escolher
        public void ShowAndChooseAttack()
        {
            ShowAttacks();
            Console.Write("\nChoose an attack (number): ");
            
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                ChooseAttack(choice - 1); // Ajusta para índice 0-based
            }
            else
            {
                Console.WriteLine("Invalid input!");
            }
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
            {
                Console.WriteLine($"{i + 1}. {Attacks[i].GetDisplay()}");
            }
        }

        public void TakeDamage(int damage)
        {
            if (Shield)
            {
                damage = Math.Max(0, damage - ProtectionOfShield);
                Console.WriteLine($"{Name} blocked {ProtectionOfShield} damage with shield!");
                Shield = false;
            }

            Hp = Math.Max(0, Hp - damage);
            Console.WriteLine($"{Name} took {damage} damage! HP: {Hp}");
        }

        public bool IsAlive()
        {
            return Hp > 0;
        }

        // Método para restaurar mana
        public void RestoreMana(int amount)
        {
            Mana = Math.Min(MaxMana, Mana + amount);
            Console.WriteLine($"{Name} restored {amount} mana! Mana: {Mana}/{MaxMana}");
        }
    }
/****************************************************FLOOR 1****************************************************/
public class Slime : Monsters
    {
        public Slime() : base("Slime", 1, 50, 40, 30)
        {
            AddAttack("Normal Attack", 20, 0);
            AddAttack("Slime Splash", 10, 5);
            AddAttack("Bouncy", 15, 10);
            AddAttack("Poison Spit", 25, 20);
            
            Shield = false;
        }

        public void AttackSlime()
        {
            Console.WriteLine($"\n=== {Name} is attacking! ===");
            
            // Criando array com os nomes dos ataques formatados
            string[] attackNames = new string[Attacks.Count];
            for (int i = 0; i < Attacks.Count; i++)
            {
                attackNames[i] = Attacks[i].GetDisplay();
            }

            // Mostrando todos os ataques
            Console.WriteLine("Available attacks:");
            foreach (string attack in attackNames)
            {
                Console.WriteLine($"- {attack}");
            }

            // Simulando um ataque aleatório
            Random rand = new Random();
            int attackIndex = rand.Next(Attacks.Count);
            Attack chosenAttack = Attacks[attackIndex];
            
            Console.WriteLine($"\n{Name} used {chosenAttack.Name}!");
            Console.WriteLine($"Dealt {chosenAttack.Damage} damage!");
        }
    }
    public class WarriorSkeleton : Monsters
    {
        public WarriorSkeleton() : base("WarriorSkeleton", 1, 70, 0, 50)
        {
            AddAttack("Normal Attack", 28, 0);
            AddAttack("Slash", 30, 0);
            AddAttack("Shield Bash Combo", 30, 0);
            
            Shield = false;
        }

        public void AttackSkeleton()
        {
            Console.WriteLine($"\n=== {Name} is attacking! ===");
            
            // Criando array com os nomes dos ataques formatados
            string[] attackNames = new string[Attacks.Count];
            for (int i = 0; i < Attacks.Count; i++)
            {
                attackNames[i] = Attacks[i].GetDisplay();
            }

            // Mostrando todos os ataques
            Console.WriteLine("Available attacks:");
            foreach (string attack in attackNames)
            {
                Console.WriteLine($"- {attack}");
            }

            // Simulando um ataque aleatório
            Random rand = new Random();
            int attackIndex = rand.Next(Attacks.Count);
            Attack chosenAttack = Attacks[attackIndex];
            
            Console.WriteLine($"\n{Name} used {chosenAttack.Name}!");
            Console.WriteLine($"Dealt {chosenAttack.Damage} damage!");
            }
        }
    
    public class MageSkeleton : Monsters
    {
        public MageSkeleton() : base("MageSkeleton", 1, 60, 90, 80)
        {
            AddAttack("fire ball", 28, 0);
            AddAttack("Slash", 30, 0);
            AddAttack("Shield Bash Combo", 30, 0);
            
            Shield = false;
        }

        public void AttackSkeleton()
        {
            Console.WriteLine($"\n=== {Name} is attacking! ===");
            
            // Criando array com os nomes dos ataques formatados
            string[] attackNames = new string[Attacks.Count];
            for (int i = 0; i < Attacks.Count; i++)
            {
                attackNames[i] = Attacks[i].GetDisplay();
            }

            // Mostrando todos os ataques
            Console.WriteLine("Available attacks:");
            foreach (string attack in attackNames)
            {
                Console.WriteLine($"- {attack}");
            }

            // Simulando um ataque aleatório
            Random rand = new Random();
            int attackIndex = rand.Next(Attacks.Count);
            Attack chosenAttack = Attacks[attackIndex];
            
            Console.WriteLine($"\n{Name} used {chosenAttack.Name}!");
            Console.WriteLine($"Dealt {chosenAttack.Damage} damage!");
        }
    }



public class GigantSpider : Monsters
    {
        public GigantSpider() : base("Gigant Spider", 1, 90, 80, 50)
        {
            AddAttack("Bite", 45, 0);
            AddAttack("web", 30, 27);
            AddAttack("poison atack", 48, 30);
            
            Shield = false;
        }

        public void AttackSpider()
        {
            Console.WriteLine($"\n=== {Name} is attacking! ===");
            
            // Criando array com os nomes dos ataques formatados
            string[] attackNames = new string[Attacks.Count];
            for (int i = 0; i < Attacks.Count; i++)
            {
                attackNames[i] = Attacks[i].GetDisplay();
            }

            // Mostrando todos os ataques
            Console.WriteLine("Available attacks:");
            foreach (string attack in attackNames)
            {
                Console.WriteLine($"- {attack}");
            }

            // Simulando um ataque aleatório
            Random rand = new Random();
            int attackIndex = rand.Next(Attacks.Count);
            Attack chosenAttack = Attacks[attackIndex];
            
            Console.WriteLine($"\n{Name} used {chosenAttack.Name}!");
            Console.WriteLine($"Dealt {chosenAttack.Damage} damage!");
        }
    }

/****************************************************FLOOR 2****************************************************/
}