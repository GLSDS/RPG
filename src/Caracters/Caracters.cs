using System;
using System.Collections.Generic;
using type;

namespace Caracters{

// Base class for all character classes
public class ClassesCaracter
{
    public string Name { get; set; } = "";
    public int Level { get; set; }
    private double hp;

    private readonly int maxHp;
    private readonly double xp;
    private readonly double maxXp;
    private int _mana;
    private readonly double maxMana;
    public double Hp
    {
        get => hp;
        set => hp = Math.Max(0, value); // Never goes below 0
    }
    public int MaxHp { get; set; }
    public int Mana
    {
        get => _mana;
        set => _mana = Math.Max(0, value);
    }
    public int Damage { get; set; }
    public int Xp { get; set; }
    public Skills CharacterSkills { get; set; } = new Skills();
    public bool IsAlive => Hp > 0;

    // Display character stats
    public virtual void DisplayStats()
    {
        Console.WriteLine($"=== {Name} ===");
        Console.WriteLine($"Level: {Level}");
        Console.WriteLine($"HP: {Hp}/{MaxHp}");
        Console.WriteLine($"Mana: {Mana}");
        Console.WriteLine($"Damage: {Damage}");
        Console.WriteLine($"XP: {Xp}");
        Console.WriteLine();

        if (CharacterSkills != null)
        {
            CharacterSkills.DisplayStats();
        }
    }

    // Gain experience and level up
    public virtual void GainXp(int amount)
    {
        Xp += amount;
        int xpNeeded = Level * 100;

        Console.WriteLine($"{Name} ganhou {amount} XP!");

        while (Xp >= xpNeeded)
        {
            LevelUp();
            xpNeeded = Level * 100;
        }
    }

    // Level up
    public virtual void LevelUp()
    {
        Level++;
        MaxHp += 50;
        Hp = MaxHp;
        Mana += 30;
        Damage += 5;
        Console.WriteLine($"🎉 {Name} subiu para o nível {Level}!");
        Console.WriteLine($"HP: {Hp}/{MaxHp}, Mana: {Mana}, Dano: {Damage}");
        Console.WriteLine();
    }

    // Attack with a skill
    public virtual void Attack(ClassesCaracter target, string skillName)
    {
        if (!IsAlive)
        {
            Console.WriteLine($"{Name} está morto e não pode atacar!");
            return;
        }

        if (!target.IsAlive)
        {
            Console.WriteLine($"{target.Name} já está morto!");
            return;
        }

        double damage = Damage;
        string skillDescription = "";
        int manaCost = 0;

        // Find the skill
        if (CharacterSkills != null)
        {
            if (CharacterSkills.Offensive1 == skillName)
            {
                damage += CharacterSkills.Offensive1Damage;
                skillDescription = CharacterSkills.Offensive1;
                manaCost = CharacterSkills.Offensive1ManaCost;
            }
            else if (CharacterSkills.Offensive2 == skillName)
            {
                damage += CharacterSkills.Offensive2Damage;
                skillDescription = CharacterSkills.Offensive2;
                manaCost = CharacterSkills.Offensive2ManaCost;
            }
            else if (CharacterSkills.Offensive3 == skillName)
            {
                damage += CharacterSkills.Offensive3Damage;
                skillDescription = CharacterSkills.Offensive3;
                manaCost = CharacterSkills.Offensive3ManaCost;
            }
            else if (CharacterSkills.Artifact == skillName)
            {
                damage += CharacterSkills.ArtifactDamage;
                skillDescription = CharacterSkills.Artifact;
                manaCost = 0;
            }
            else
            {
                skillDescription = "Ataque Básico";
            }
        }
        else
        {
            skillDescription = "Ataque Básico";
        }

        // Check if has enough mana
        if (manaCost > 0 && Mana < manaCost)
        {
            Console.WriteLine($"{Name} não tem mana suficiente para usar {skillDescription}!");
            skillDescription = "Ataque Básico";
            damage = Damage;
        }
        else
        {
            Mana -= manaCost;
        }

        // Apply passive effects if any
        if (CharacterSkills != null && CharacterSkills.PassiveEffect > 0)
        {
            if (Hp <= MaxHp * 0.3) // Below 30% HP
            {
                damage *= (1 + CharacterSkills.PassiveEffect / 100);
                Console.WriteLine($"🔥 {CharacterSkills.Passive} ativado! Dano aumentado em {CharacterSkills.PassiveEffect}%");
            }
        }

        Console.WriteLine($"{Name} usou {skillDescription} causando {(int)damage} de dano!");
        target.TakeDamage((int)damage);
    }

    // Take damage
    public virtual void TakeDamage(int damage)
    {
        // Apply damage reduction from passive if any
        if (CharacterSkills != null && CharacterSkills.PassiveEffect > 0)
        {
            // Warrior shield wall reduces damage
            if (this is Warrior)
            {
                int reducedDamage = (int)(damage * (1 - CharacterSkills.PassiveEffect / 100));
                Console.WriteLine($"🛡️ {CharacterSkills.Passive} reduziu o dano de {damage} para {reducedDamage}");
                damage = reducedDamage;
            }
        }

        Hp -= damage;
        if (Hp < 0) Hp = 0;

        Console.WriteLine($"{Name} recebeu {damage} de dano! HP: {Hp}/{MaxHp}");

        if (!IsAlive)
        {
            Console.WriteLine($"💀 {Name} foi derrotado!");
            Console.WriteLine();
        }
    }

    // Heal
    public virtual void Heal(int amount)
    {
        // Apply healing bonus from passive if any
        if (CharacterSkills != null && CharacterSkills.PassiveEffect > 0)
        {
            if (this is Cleric)
            {
                amount = (int)(amount * (1 + CharacterSkills.PassiveEffect / 100));
                Console.WriteLine($"✨ {CharacterSkills.Passive} aumentou a cura para {amount}");
            }
        }

        Hp += amount;
        if (Hp > MaxHp) Hp = MaxHp;
        Console.WriteLine($"{Name} recuperou {amount} HP! HP: {Hp}/{MaxHp}");
    }

    // Restore mana
    public virtual void RestoreMana(int amount)
    {
        Mana += amount;
        if (Mana > 1000) Mana = 1000; // Max mana cap
        Console.WriteLine($"{Name} recuperou {amount} de mana! Mana: {Mana}");
    }
}

// Skills class
public class Skills
{
    public string Passive { get; set; } = "";
    public string PassiveDescription { get; set; } = "";
    public int PassiveEffect { get; set; }

    public string Offensive1 { get; set; } = "";
    public int Offensive1ManaCost { get; set; }
    public int Offensive1Damage { get; set; }
    public String offensive1Type { get; set; } = "";

    public string Offensive2 { get; set; } = "";
    public int Offensive2ManaCost { get; set; }
    public int Offensive2Damage { get; set; }
    public String offensive2Type { get; set; } = "";

    public string Offensive3 { get; set; } = "";
    public int Offensive3ManaCost { get; set; }
    public int Offensive3Damage { get; set; }
    public String offensive3Type { get; set; } = "";

    public string Artifact { get; set; } = "";
    public string ArtifactDescription { get; set; } = "";
    public int ArtifactDamage { get; set; }
    public int ArtifactHeal { get; set; }

    public virtual void DisplayStats()
    {
        Console.WriteLine("=== SKILLS ===");
        Console.WriteLine($"Passive: {Passive}");
        Console.WriteLine($"Description: {PassiveDescription}");
        Console.WriteLine($"Effect: {PassiveEffect}%");
        Console.WriteLine();

        Console.WriteLine($"Offensive 1: {Offensive1}");
        Console.WriteLine($"  Damage: {Offensive1Damage}, Mana Cost: {Offensive1ManaCost}");
        Console.WriteLine();

        Console.WriteLine($"Offensive 2: {Offensive2}");
        Console.WriteLine($"  Damage: {Offensive2Damage}, Mana Cost: {Offensive2ManaCost}");
        Console.WriteLine();

        Console.WriteLine($"Offensive 3: {Offensive3}");
        Console.WriteLine($"  Damage: {Offensive3Damage}, Mana Cost: {Offensive3ManaCost}");
        Console.WriteLine();

        if (!string.IsNullOrEmpty(Artifact))
        {
            Console.WriteLine($"Artifact: {Artifact}");
            Console.WriteLine($"Description: {ArtifactDescription}");
            Console.WriteLine($"Damage: {ArtifactDamage}, Heal: {ArtifactHeal}");
            Console.WriteLine();
        }
        Console.WriteLine("==================");
        Console.WriteLine();
    }

    public void SetArtifact(string name, string description, int damage, int heal)
    {
        Artifact = name;
        ArtifactDescription = description;
        ArtifactDamage = damage;
        ArtifactHeal = heal;
    }
}

// Mage class
public class Mage : ClassesCaracter
{
    public Mage()
    {
        Name = "";
        Level = 1;
        Hp = 320;
        MaxHp = 320;
        Mana = 600;
        Damage = 45;
        Xp = 0;
        CharacterSkills = new SkillsMage();
    }

    public Mage(string name, int level, int hp, int maxHp, int mana, int damage, int xp)
    {
        Name = name;
        Level = level;
        Hp = hp;
        MaxHp = maxHp;
        Mana = mana;
        Damage = damage;
        Xp = xp;
        CharacterSkills = new SkillsMage();
    }

    public class SkillsMage : Skills
    {
        public SkillsMage()
        {
            Passive = "Mana Shield";
            PassiveDescription = "Absorbs damage using mana";
            PassiveEffect = 20;

            Offensive1 = "Fireball";
            Offensive1ManaCost = 10;
            Offensive1Damage = 30;

            Offensive2 = "Ice Lance";
            Offensive2ManaCost = 40;
            Offensive2Damage = 70;

            Offensive3 = "Arcane Blast";
            Offensive3ManaCost = 60;
            Offensive3Damage = 120;

            Artifact = "Staff of the Arcane";
            ArtifactDescription = "A powerful staff that amplifies magical abilities";
            ArtifactDamage = 150;
            ArtifactHeal = 0;
        }
    }
}

// Barbarian class
public class Barbarian : ClassesCaracter
{
    public Barbarian()
    {
        Name = "";
        Level = 1;
        Hp = 650;
        MaxHp = 650;
        Mana = 0;
        Damage = 55;
        Xp = 0;
        CharacterSkills = new SkillsBarbarian();
    }

    public Barbarian(string name, int level, int hp, int maxHp, int mana, int damage, int xp)
    {
        Name = name;
        Level = level;
        Hp = hp;
        MaxHp = maxHp;
        Mana = mana;
        Damage = damage;
        Xp = xp;
        CharacterSkills = new SkillsBarbarian();
    }

    public class SkillsBarbarian : Skills
    {
        public SkillsBarbarian()
        {
            Passive = "Berserker Rage";
            PassiveDescription = "Increases damage output when health is low";
            PassiveEffect = 20;

            Offensive1 = "Cleave";
            Offensive1ManaCost = 0;
            Offensive1Damage = 80;

            Offensive2 = "Whirlwind";
            Offensive2ManaCost = 0;
            Offensive2Damage = 60;

            Offensive3 = "Earthshatter";
            Offensive3ManaCost = 0;
            Offensive3Damage = 20;

            Artifact = "Axe of the Berserker";
            ArtifactDescription = "A massive axe that grows stronger in battle";
            ArtifactDamage = 200;
            ArtifactHeal = 0;
        }
    }
}

// Warrior class
public class Warrior : ClassesCaracter
{
    public Warrior()
    {
        Name = "";
        Level = 1;
        Hp = 450;
        MaxHp = 450;
        Mana = 100;
        Damage = 40;
        Xp = 0;
        CharacterSkills = new SkillsWarrior();
    }

    public Warrior(string name, int level, int hp, int maxHp, int mana, int damage, int xp)
    {
        Name = name;
        Level = level;
        Hp = hp;
        MaxHp = maxHp;
        Mana = mana;
        Damage = damage;
        Xp = xp;
        CharacterSkills = new SkillsWarrior();
    }

    public class SkillsWarrior : Skills
    {
        public SkillsWarrior()
        {
            Passive = "Shield Wall";
            PassiveDescription = "Reduces incoming damage for a short duration";
            PassiveEffect = 30;

            Offensive1 = "Shield Bash";
            Offensive1ManaCost = 20;
            Offensive1Damage = 50;

            Offensive2 = "Power Strike";
            Offensive2ManaCost = 30;
            Offensive2Damage = 80;

            Offensive3 = "Battle Cry";
            Offensive3ManaCost = 50;
            Offensive3Damage = 0;

            Artifact = "Sword of Justice";
            ArtifactDescription = "A legendary sword that protects the innocent";
            ArtifactDamage = 180;
            ArtifactHeal = 50;
        }
    }
}

// Rogue class
public class Rogue : ClassesCaracter
{
    public Rogue()
    {
        Name = "";
        Level = 1;
        Hp = 230;
        MaxHp = 230;
        Mana = 70;
        Damage = 50;
        Xp = 0;
        CharacterSkills = new SkillsRogue();
    }

    public Rogue(string name, int level, int hp, int maxHp, int mana, int damage, int xp)
    {
        Name = name;
        Level = level;
        Hp = hp;
        MaxHp = maxHp;
        Mana = mana;
        Damage = damage;
        Xp = xp;
        CharacterSkills = new SkillsRogue();
    }

    public class SkillsRogue : Skills
    {
        public SkillsRogue()
        {
            Passive = "Shadow Step";
            PassiveDescription = "Increases dodge chance and movement speed";
            PassiveEffect = 15;

            Offensive1 = "Fast Attack";
            Offensive1ManaCost = 20;
            Offensive1Damage = 50;

            Offensive2 = "Poisoned Dagger";
            Offensive2ManaCost = 0;
            Offensive2Damage = 90;

            Offensive3 = "Attack from the Shadows";
            Offensive3ManaCost = 90;
            Offensive3Damage = 120;

            Artifact = "Dagger of Shadows";
            ArtifactDescription = "A blade that strikes from the darkness";
            ArtifactDamage = 220;
            ArtifactHeal = 0;
        }
    }
}

// Cleric class
public class Cleric : ClassesCaracter
{
    public Cleric()
    {
        Name = "";
        Level = 1;
        Hp = 230;
        MaxHp = 230;
        Mana = 530;
        Damage = 35;
        Xp = 0;
        CharacterSkills = new SkillsCleric();
    }

    public Cleric(string name, int level, int hp, int maxHp, int mana, int damage, int xp)
    {
        Name = name;
        Level = level;
        Hp = hp;
        MaxHp = maxHp;
        Mana = mana;
        Damage = damage;
        Xp = xp;
        CharacterSkills = new SkillsCleric();
    }

    public class SkillsCleric : Skills
    {
        public SkillsCleric()
        {
            Passive = "Holy Aura";
            PassiveDescription = "Increases healing received and reduces damage taken";
            PassiveEffect = 20;

            Offensive1 = "Smite";
            Offensive1ManaCost = 30;
            Offensive1Damage = 70;

            Offensive2 = "Holy Light";
            Offensive2ManaCost = 40;
            Offensive2Damage = 90;

            Offensive3 = "Divine Storm";
            Offensive3ManaCost = 50;
            Offensive3Damage = 110;

            Artifact = "Holy Book of Light";
            ArtifactDescription = "An ancient tome filled with divine power";
            ArtifactDamage = 100;
            ArtifactHeal = 200;
        }
    }
}

// Character Factory
public static class CharacterFactory
{
    public static ClassesCaracter CreateCharacter()
    {
        Console.WriteLine("╔═══════════════════════════════╗");
        Console.WriteLine("║    ESCOLHA SUA CLASSE        ║");
        Console.WriteLine("╠═══════════════════════════════╣");
        Console.WriteLine("║ 1 - 🧙 Mago                 ║");
        Console.WriteLine("║ 2 - 🪓 Bárbaro              ║");
        Console.WriteLine("║ 3 - ⚔️ Guerreiro             ║");
        Console.WriteLine("║ 4 - 🗡️ Ladino               ║");
        Console.WriteLine("║ 5 - ✨ Clérigo              ║");
        Console.WriteLine("╚═══════════════════════════════╝");
        Console.Write("Opção: ");

        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5)
        {
            Console.Write("Opção inválida! Escolha 1-5: ");
        }

        Console.Write("Digite o nome do personagem: ");
        string name = Console.ReadLine() ?? "";

        ClassesCaracter character = choice switch
        {
            1 => new Mage(),
            2 => new Barbarian(),
            3 => new Warrior(),
            4 => new Rogue(),
            5 => new Cleric(),
            _ => throw new ArgumentException("Classe inválida!")
        };

        character.Name = name;
        return character;
        }
    }
}