using System;

public class ChoiceLevel
    {
        public int Level { get; }
        public double MaxHp { get; }
        public double MaxMana { get; }
        public double Xp { get; }

        // Construtor sem parâmetros: gera level aleatório E calcula os stats.
        public ChoiceLevel() : this(Random.Shared.Next(1, 11)) { }

        // Construtor por level: calcula os stats com base na fórmula.
        public ChoiceLevel(int level)
        {
            if (level < 1) level = 1;
            if (level > 10) level = 10;

            Level = level;

            double baseHp = 40 + Level * 10;
            double baseMana = 20 + Level * 6;
            double baseXp = 15 + Level * 8;

            MaxHp = baseHp + baseHp * 0.4;
            MaxMana = baseMana + baseMana * 0.2;
            Xp = baseXp + baseXp * 0.9;
        }

        // Construtor completo (mantido para compatibilidade).
        public ChoiceLevel(int level, double maxHp, double maxMana, double xp)
        {
            Level = level;
            MaxHp = maxHp;
            MaxMana = maxMana;
            Xp = xp;
        }
    }