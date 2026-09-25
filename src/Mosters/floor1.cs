using System;
using Monsters;
using System.Collections.Generic;
using type_element;

namespace floor1
{
    public class Slime : Monster
    {
        public Slime(ChoiceLevel? choice = null)
            : base("Slime",
                   choice?.Level ?? 1,
                   choice?.MaxHp ?? 50,
                   choice?.MaxMana ?? 30,
                   choice?.Xp ?? 20,
                   type: "Beast",
                   element: TypeElement.Poison)   // Slime cospe veneno
        {
            AddAttack("Normal Attack", 20, 0, TypeElement.Water);
            AddAttack("Slime Splash", 10, 5, TypeElement.Water);
            AddAttack("Bouncy", 15, 10, TypeElement.Poison);
            AddAttack("Poison Spit", 25, 10, TypeElement.Poison);
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
                   type: "Undead",
                   element: TypeElement.Dark)    // morto-vivo → trevas
        {
            AddAttack("Normal Attack", 28, 0);
            AddAttack("Slash", 30, 0, TypeElement.Fight);
            AddAttack("Shield Bash Combo", 30, 20, TypeElement.Fight, true);
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
                   type: "Undead",
                   element: TypeElement.Dark)
        {
            AddAttack("Fire Ball", 28, 0, TypeElement.Fire);
            AddAttack("Pump", 30, 0, TypeElement.Fire);
            AddAttack("Necromancy", 80, 50, TypeElement.Dark);
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
                   type: "Beast",
                   element: TypeElement.Poison)  // aranha peçonhenta
        {
            AddAttack("Bite", 45, 0);
            AddAttack("Web", 30, 27, TypeElement.Bug);
            AddAttack("Poison Attack", 48, 30, TypeElement.Poison);
        }
    }
}