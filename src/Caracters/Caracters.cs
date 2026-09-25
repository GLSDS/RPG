using System;

namespace Characters
{
    public static class CharacterFactory
    {
        public static CharacterClass CreateCharacter()
        {
            Console.WriteLine("Escolha sua classe: 1-Mage, 2-Barbarian, 3-Warrior, 4-Rogue, 5-Cleric");
            string classChoice = Console.ReadLine() ?? "3";

            Console.Write("Nome do personagem: ");
            string name = Console.ReadLine() ?? "Aventureiro";

            return classChoice.Trim().ToLowerInvariant() switch
            {
                "1" or "mage"      => new Mage(name),
                "2" or "barbarian" => new Barbarian(name),
                "4" or "rogue"     => new Rogue(name),
                "5" or "cleric"    => new Cleric(name),
                _                  => new Warrior(name)
            };
        }
    }
}