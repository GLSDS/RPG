using System;
using Characters;
using items;
using type_item;
using Monsters;
using floor1;
using type_element;
namespace Program
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "RPG Character System";

            Console.WriteLine("╔═══════════════════════════════════════════════════╗");
            Console.WriteLine("║            🎮 RPG CHARACTER SYSTEM               ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════╝");
            Console.WriteLine();

            // Create player character
            var player = CharacterFactory.CreateCharacter();

            if (!Console.IsOutputRedirected)
                Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════╗");
            Console.WriteLine("║    PERSONAGEM CRIADO COM SUCESSO  ║");
            Console.WriteLine("╚═══════════════════════════════════╝");
            Console.WriteLine();

            player.DisplayStats();

            // Create an enemy
            Monster enemy = new GiantSpider(new ChoiceLevel(6));

            Console.WriteLine("\n╔═══════════════════════════════════════════════════╗");
            Console.WriteLine("║              ⚔️ SIMULAÇÃO DE BATALHA            ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════╝");
            Console.WriteLine();

            Console.WriteLine($"⚔️ {player.Name} encontrou um {enemy.Name}!");
            Console.WriteLine();


            // Battle simulation
            bool playerTurn = true;
            Random rand = new();
            int turn = 1;


            if(player.Hp < player.MaxHp * 0.1) // ver se hp esta muito baixo para iniciar a batalha
            {
                Console.WriteLine("O jogador não tem HP suficiente para iniciar a batalha.");
                return;
            }

            while (player.IsAlive && enemy.IsAlive())
            {
                Console.WriteLine($"\n--- Turno {turn} ---");
                Console.WriteLine($"🧙 {player.Name}: HP {player.Hp}/{player.MaxHp} | Mana {player.Mana}");
                Console.WriteLine($"👹 {enemy.Name}: HP {enemy.Hp}/{enemy.MaxHp}");
                Console.WriteLine();

                if (playerTurn)
                {
                    // Player attacks
                    Console.WriteLine("Escolha uma ação:");
                    Console.WriteLine("1 - Ataque Básico");
                    Console.WriteLine("2 - Usar Habilidade (Aleatória)");
                    Console.WriteLine("3 - Inventory"); // fix items system
                    Console.WriteLine("4 - Mostrar Stats");
                    Console.Write("Opção: ");

                    string action = Console.ReadLine() ?? "";
                    Console.WriteLine();

                    switch (action)
                    {
                        case "1":
                            player.Attack(enemy, "Ataque Básico");
                            break;
                        case "2":
                            // Random skill
                            string[] skills = player.Attacks.Select(attack => attack.Name).ToArray();
                            if (skills.Length == 0)
                            {
                                Console.WriteLine("Este personagem não possui ataques.");
                                break;
                            }
                            string skill = skills[rand.Next(skills.Length)];
                            player.Attack(enemy, skill);
                            break;
                        case "3":
                            var inventoryProperty = player.GetType().GetProperty("Inventory");
                            if (inventoryProperty is not null)
                            {
                                var inventory = inventoryProperty.GetValue(player);
                                var listarMethod = inventory?.GetType().GetMethod("Listar");

                                if (inventory is not null && listarMethod is not null)
                                {
                                    listarMethod.Invoke(inventory, null);
                                }
                                else
                                {
                                    Console.WriteLine("Inventário indisponível para este personagem.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Inventário indisponível para este personagem.");
                            }
                            break;
                        case "4":
                            player.DisplayStats();
                            break;
                        default:
                            Console.WriteLine("Ação inválida! Atacando basicamente.");
                            player.Attack(enemy, "Ataque Básico");
                            break;
                    }

                    // Gain XP if enemy dies
                    if (!enemy.IsAlive())
                    {
                        player.GainXp((int)enemy.Xp);
                    }
                }
                else
                {
                    // Enemy attacks
                    Console.WriteLine($"👹 {enemy.Name} ataca!");
                    enemy.PerformRandomAttack(player);
                }

                playerTurn = !playerTurn;
                turn++;

                if (player.IsAlive && enemy.IsAlive())
                {
                    Console.WriteLine("\nPressione Enter para continuar...");
                    Console.ReadLine();
                }
                if (!Console.IsOutputRedirected)
                    Console.Clear();
            }

        }
    }
}