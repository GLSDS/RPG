using System;

namespace Back_end_RPG.caracter
{
    public class Caracters
    {
        class wizard
        {
            int level = 1;
            int Hp = 320;
            int mana = 600;            
            double damage = 32.3;
            double Xp = 0;   

            public wizard(int level, int hp, int mana, double damage, double xp)
            {
                wizardLevel = level;
                wizardHp = hp;
                wizardMana = mana;
                wizardDamage = damage;
                wizardXp = xp;
            }
        }

        public void DisplayStats()
        {
            Console.WriteLine($"level: {level}\n HP: {hp}\n Mana: {mana}\n Damage: {damage}\n Xp: {xp}\n");
        }
    }
}