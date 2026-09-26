using System;

namespace Characters
{
    public static class CharacterFactory
    {
        public static CharacterClass CreateCharacter()
        {
            Console.WriteLine("Escolha sua classe:\n 1-Mage\n 2-Barbarian\n 3-Warrior\n 4-Rogue\n 5-Cleric\n");
            string classChoice = Console.ReadLine() ?? "3";

            Console.Write("Nome do personagem: ");
            string name = Console.ReadLine() ?? "Aventureiro";

            return classChoice.Trim().ToLowerInvariant() switch
            {
                "1" or "mage"      => new Mage(name),
                "2" or "barbarian" => new Barbarian(name),
                "3" or "warrior"    => new Warrior(name),
                "4" or "rogue"     => new Rogue(name),
                "5" or "cleric"    => new Cleric(name),
                _                  => new Warrior(name)
            };
        }
    }
}