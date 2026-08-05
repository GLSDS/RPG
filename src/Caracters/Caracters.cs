using System;

namespace Back_end_RPG.caracter
{
    public class Characters
    {
        // Base class for all character classes
        public class ClassesCaracter
        {
            public string Name { get; set; }
            public int Level { get; set; }
            public int Hp { get; set; }
            public double Mana { get; set; }
            public double Damage { get; set; }
            public double Xp { get; set; }
            public Skills CharacterSkills { get; set; }

            public virtual void DisplayStats()
            {
                Console.WriteLine($"Name: {Name}");
                Console.WriteLine($"Level: {Level}");
                Console.WriteLine($"HP: {Hp}");
                Console.WriteLine($"Mana: {Mana}");
                Console.WriteLine($"Damage: {Damage}");
                Console.WriteLine($"XP: {Xp}");
                Console.WriteLine();
                
                if (CharacterSkills != null)
                {
                    CharacterSkills.DisplayStats();
                }
            }
        }

        // Skills class
        public class Skills
        {
            public string Passive { get; set; }
                public string PassiveDescription { get; set; }
                    public double PassiveEffect { get; set; }

            public string Offensive1 { get; set; }
                public int Offensive1ManaCost { get; set; }
                    public double Offensive1Damage { get; set; }

            public string Offensive2 { get; set; }
                public int Offensive2ManaCost { get; set; }
                    public double Offensive2Damage { get; set; }

            public string Offensive3 { get; set; }
                public int Offensive3ManaCost { get; set; }
                    public double Offensive3Damage { get; set; }

            public string Artifact { get; set; }
                public string ArtifactDescription { get; set; }
                    public double ArtifactDamage { get; set; }
                        public double ArtifactHeal { get; set; }

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
                }
                Console.WriteLine("==================");
                Console.WriteLine();
            }
        }

        // Mage class
        public class Mage : ClassesCaracter
        {
            // Default constructor
            public Mage()
            {
                Name = "";
                Level = 1;
                Hp = 320;
                Mana = 600;
                Damage = 32.3;
                Xp = 0;
                CharacterSkills = new SkillsMage();
            }

            // Parameterized constructor
            public Mage(string name, int level, int hp, int mana, double damage, double xp)
            {
                Name = name;
                Level = level;
                Hp = hp;
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
                    PassiveEffect = 20; // Reduces damage taken by 20%

                    Offensive1 = "Fireball";
                    Offensive1ManaCost = 10;
                    Offensive1Damage = 30;

                    Offensive2 = "Ice Lance";
                    Offensive2ManaCost = 40;
                    Offensive2Damage = 70;

                    Offensive3 = "Arcane Blast";
                    Offensive3ManaCost = 60;
                    Offensive3Damage = 120;

                    Artifact = "";
                    ArtifactDescription = "";
                    ArtifactDamage = 0;
                    ArtifactHeal = 0;
                }
            }
        }

        // Barbarian class
        public class Barbarian : ClassesCaracter
        {
            // Default constructor
            public Barbarian()
            {
                Name = "";
                Level = 1;
                Hp = 650;
                Mana = 0;
                Damage = 92.3;
                Xp = 0;
                CharacterSkills = new SkillsBarbarian();
            }

            // Parameterized constructor
            public Barbarian(string name, int level, int hp, int mana, double damage, double xp)
            {
                Name = name;
                Level = level;
                Hp = hp;
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
                    PassiveEffect = 20; // Increases damage by 20% when below 30% health

                    Offensive1 = "Cleave";
                    Offensive1ManaCost = 0;
                    Offensive1Damage = 80;

                    Offensive2 = "Whirlwind";
                    Offensive2ManaCost = 0;
                    Offensive2Damage = 60;

                    Offensive3 = "Earthshatter";
                    Offensive3ManaCost = 0;
                    Offensive3Damage = 20;

                    Artifact = "";
                    ArtifactDescription = "";
                    ArtifactDamage = 0;
                    ArtifactHeal = 0;
                }
            }
        }

        // Warrior class
        public class Warrior : ClassesCaracter
        {
            // Default constructor
            public Warrior()
            {
                Name = "";
                Level = 1;
                Hp = 450;
                Mana = 100;
                Damage = 49.3;
                Xp = 0;
                CharacterSkills = new SkillsWarrior();
            }

            // Parameterized constructor
            public Warrior(string name, int level, int hp, int mana, double damage, double xp)
            {
                Name = name;
                Level = level;
                Hp = hp;
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
                    PassiveEffect = 30; // Reduces damage taken by 30% for 5 seconds

                    Offensive1 = "Shield Bash";
                    Offensive1ManaCost = 20;
                    Offensive1Damage = 50;

                    Offensive2 = "Power Strike";
                    Offensive2ManaCost = 30;
                    Offensive2Damage = 80;

                    Offensive3 = "Battle Cry";
                    Offensive3ManaCost = 50;
                    Offensive3Damage = 0;

                    Artifact = "";
                    ArtifactDescription = "";
                    ArtifactDamage = 0;
                    ArtifactHeal = 0;
                }
            }
        }

        // Rogue class
        public class Rogue : ClassesCaracter
        {
            // Default constructor
            public Rogue()
            {
                Name = "";
                Level = 1;
                Hp = 230;
                Mana = 70;
                Damage = 26.3;
                Xp = 0;
                CharacterSkills = new SkillsRogue();
            }

            // Parameterized constructor
            public Rogue(string name, int level, int hp, int mana, double damage, double xp)
            {
                Name = name;
                Level = level;
                Hp = hp;
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
                    PassiveEffect = 15; // Increases dodge chance by 15%

                    Offensive1 = "Fast Attack";
                    Offensive1ManaCost = 20;
                    Offensive1Damage = 50;

                    Offensive2 = "Poisoned Dagger";
                    Offensive2ManaCost = 0;
                    Offensive2Damage = 90;

                    Offensive3 = "Attack from the Shadows";
                    Offensive3ManaCost = 90;
                    Offensive3Damage = 120;

                    Artifact = "";
                    ArtifactDescription = "";
                    ArtifactDamage = 0;
                    ArtifactHeal = 0;
                }
            }
        }

        // Cleric class
        public class Cleric : ClassesCaracter
        {
            // Default constructor
            public Cleric()
            {
                Name = "";
                Level = 1;
                Hp = 230;
                Mana = 530;
                Damage = 17.2;
                Xp = 0;
                CharacterSkills = new SkillsCleric();
            }

            // Parameterized constructor
            public Cleric(string name, int level, int hp, int mana, double damage, double xp)
            {
                Name = name;
                Level = level;
                Hp = hp;
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
                    PassiveEffect = 20; // Increases healing received by 20% and reduces damage taken by 10%

                    Offensive1 = "Smite";
                    Offensive1ManaCost = 30;
                    Offensive1Damage = 70;

                    Offensive2 = "Holy Light";
                    Offensive2ManaCost = 40;
                    Offensive2Damage = 90;

                    Offensive3 = "Divine Storm";
                    Offensive3ManaCost = 50;
                    Offensive3Damage = 110;

                    Artifact = "";
                    ArtifactDescription = "";
                    ArtifactDamage = 0;
                    ArtifactHeal = 0;
                }
            }
        }
    }
}