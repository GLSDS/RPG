using System;
using Monsters;

namespace floor1
{
    // ─────────────────────────────────────────────────────────────
    // FLOOR 1
    // ─────────────────────────────────────────────────────────────
    public class Slime : Monster
    {
        public Slime(ChoiceLevel? choice = null)
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
        public WarriorSkeleton(ChoiceLevel? choice = null)
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
        public MageSkeleton(ChoiceLevel? choice = null)
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
        public GiantSpider(ChoiceLevel? choice = null)
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
}