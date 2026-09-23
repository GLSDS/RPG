using System;
using Monsters;


namespace floor2
{

    //<summary>
// ─────────────────────────────────────────────────────────────
// FLOOR 2
// ─────────────────────────────────────────────────────────────
//</summary>
    public class IceGolem : Monster
    {
        public IceGolem(ChoiceLevel? choice = null)
            : base("Ice Golem",
                   choice?.Level ?? 3,
                   choice?.MaxHp ?? 200,
                   choice?.MaxMana ?? 40,
                   choice?.Xp ?? 300,
                   type: "Water")
        {
            AddAttack("Ice Slam", 80, 0);
            AddAttack("Frost Punch", 60, 10);
            AddAttack("Ice Shield", 0, 30, grantsShield: true);
        }
    }


    public class IceBear : Monster
    {
        public IceBear(ChoiceLevel? choice = null)
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
        public IceWizard(ChoiceLevel? choice = null)
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
}
