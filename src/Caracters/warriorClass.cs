using type_element;

namespace Characters
{
    // ─────────────────────────────────────────────────────────────
    // MAGE
    // ─────────────────────────────────────────────────────────────
    public class Mage : CharacterClass
    {
        public Mage() : this("") { }
        public Mage(string name) : base(new CharacterData(name, "Mage", 1, 320, 320, 600, 600, 45, element: TypeElement.Light)) { }
        public Mage(CharacterData data) : base(data) { }

        protected override void RegisterAttacks()
        {
            AddAttack("Fireball",      30, 10, TypeElement.Fire);
            AddAttack("Ice Lance",     70, 40, TypeElement.Ice);
            AddAttack("Arcane Blast", 120, 60, TypeElement.Light);
            AddAttack("Staff of the Arcane", 150, 80, TypeElement.Light); // artefato como ataque
        }
    }

    // ─────────────────────────────────────────────────────────────
    // BARBARIAN
    // ─────────────────────────────────────────────────────────────
    public class Barbarian : CharacterClass
    {
        public Barbarian() : this("") { }
        public Barbarian(string name) : base(new CharacterData(name, "Barbarian", 1, 650, 650, 0, 0, 55, element: TypeElement.Fight)) { }
        public Barbarian(CharacterData data) : base(data) { }

        protected override void RegisterAttacks()
        {
            AddAttack("Cleave",       80, 0, TypeElement.Fight);
            AddAttack("Whirlwind",    60, 0, TypeElement.Air);
            AddAttack("Earthshatter", 20, 0, TypeElement.Plant);
            AddAttack("Axe of the Berserker", 200, 0, TypeElement.Fight);
        }
    }

    // ─────────────────────────────────────────────────────────────
    // WARRIOR
    // ─────────────────────────────────────────────────────────────
    public class Warrior : CharacterClass
    {
        public Warrior() : this("") { }
        public Warrior(string name) : base(new CharacterData(name, "Warrior", 1, 450, 450, 100, 100, 40, element: TypeElement.Fight)) { }
        public Warrior(CharacterData data) : base(data) { }

        protected override void RegisterAttacks()
        {
            AddAttack("Shield Bash",  50, 20, TypeElement.Fight, grantsShield: true);
            AddAttack("Power Strike", 80, 30, TypeElement.Fight);
            AddAttack("Battle Cry",    0, 50, TypeElement.Normal); // buff/sem dano
            AddAttack("Sword of Justice", 180, 60, TypeElement.Light);
        }
    }

    // ─────────────────────────────────────────────────────────────
    // ROGUE
    // ─────────────────────────────────────────────────────────────
    public class Rogue : CharacterClass
    {
        public Rogue() : this("") { }
        public Rogue(string name) : base(new CharacterData(name, "Rogue", 1, 230, 230, 70, 70, 50, element: TypeElement.Dark)) { }
        public Rogue(CharacterData data) : base(data) { }

        protected override void RegisterAttacks()
        {
            AddAttack("Fast Attack",          50, 20, TypeElement.Normal);
            AddAttack("Poisoned Dagger",      90,  0, TypeElement.Poison);
            AddAttack("Attack from the Shadows", 120, 90, TypeElement.Dark);
            AddAttack("Dagger of Shadows",   220, 70, TypeElement.Dark);
        }
    }

    // ─────────────────────────────────────────────────────────────
    // CLERIC
    // ─────────────────────────────────────────────────────────────
    public class Cleric : CharacterClass
    {
        public Cleric() : this("") { }
        public Cleric(string name) : base(new CharacterData(name, "Cleric", 1, 230, 230, 530, 530, 35, element: TypeElement.Light)) { }
        public Cleric(CharacterData data) : base(data) { }

        protected override void RegisterAttacks()
        {
            AddAttack("Smite",         70, 30, TypeElement.Light);
            AddAttack("Holy Light",    90, 40, TypeElement.Light);
            AddAttack("Divine Storm", 110, 50, TypeElement.Light);
            AddAttack("Holy Book of Light", 100, 60, TypeElement.Light, grantsShield: true);
        }
    }
}