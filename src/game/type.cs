using System;
using System.Collections.Generic;

namespace type
{
    public enum ElementType
    {
        Fire,
        Water,
        Plant,      // equivalente a "Ground"
        Air,        // equivalente a "Flying"
        Eletric,  
        Ice,
        Light,
        Dark,
        Fight,      // equivalente a "Fighting"
        Bug,
        Poison
    }

    public class TypeEffectiveness
    {
        // Tabela de efetividade: chave = (ataque, defesa), valor = multiplicador
        private readonly Dictionary<(ElementType, ElementType), double> effectivenessTable;

        // Multiplicador padrão quando não há entrada na tabela
        private const double DefaultMultiplier = 1.0;

        public TypeEffectiveness()
        {
            effectivenessTable = new Dictionary<(ElementType, ElementType), double>();

            // Fire
            AddEffectiveness(ElementType.Fire, ElementType.Bug, 2.0);
            AddEffectiveness(ElementType.Fire, ElementType.Water, 0.5);
            AddEffectiveness(ElementType.Fire, ElementType.Plant, 0.5);

            // Water
            AddEffectiveness(ElementType.Water, ElementType.Fire, 2.0);
            AddEffectiveness(ElementType.Water, ElementType.Plant, 2.0);
            AddEffectiveness(ElementType.Water, ElementType.Eletric, 0.5);

            // Plant
            AddEffectiveness(ElementType.Plant, ElementType.Eletric, 2.0);
            AddEffectiveness(ElementType.Plant, ElementType.Air, 0.0); // Imune
            AddEffectiveness(ElementType.Plant, ElementType.Fire, 2.0);

            // Air
            AddEffectiveness(ElementType.Air, ElementType.Plant, 0.0); // Imune
            AddEffectiveness(ElementType.Air, ElementType.Fight, 2.0);
            AddEffectiveness(ElementType.Air, ElementType.Bug, 2.0);

            // Eletric
            AddEffectiveness(ElementType.Eletric, ElementType.Water, 2.0);
            AddEffectiveness(ElementType.Eletric, ElementType.Air, 2.0);
            AddEffectiveness(ElementType.Eletric, ElementType.Plant, 0.0); // Imune

            // Ice
            AddEffectiveness(ElementType.Ice, ElementType.Air, 2.0);
            AddEffectiveness(ElementType.Ice, ElementType.Plant, 2.0);
            AddEffectiveness(ElementType.Ice, ElementType.Fire, 0.5);

            // Light
            AddEffectiveness(ElementType.Light, ElementType.Dark, 2.0);
            AddEffectiveness(ElementType.Light, ElementType.Poison, 2.0);
            AddEffectiveness(ElementType.Light, ElementType.Bug, 0.5);

            // Dark
            AddEffectiveness(ElementType.Dark, ElementType.Light, 2.0);
            AddEffectiveness(ElementType.Dark, ElementType.Fight, 0.5);

            // Fight
            AddEffectiveness(ElementType.Fight, ElementType.Dark, 2.0);
            AddEffectiveness(ElementType.Fight, ElementType.Ice, 2.0);
            AddEffectiveness(ElementType.Fight, ElementType.Poison, 0.5);

            // Bug
            AddEffectiveness(ElementType.Bug, ElementType.Dark, 2.0);
            AddEffectiveness(ElementType.Bug, ElementType.Fire, 0.5);
            AddEffectiveness(ElementType.Bug, ElementType.Fight, 0.5);

            // Poison
            AddEffectiveness(ElementType.Poison, ElementType.Plant, 2.0);
            AddEffectiveness(ElementType.Poison, ElementType.Fire, 0.5);
            AddEffectiveness(ElementType.Poison, ElementType.Eletric, 0.5);
        }

        /// <summary>
        /// Registra (ou sobrescreve) o multiplicador de efetividade entre dois tipos.
        /// </summary>
        private void AddEffectiveness(ElementType attack, ElementType defense, double multiplier)
        {
            effectivenessTable[(attack, defense)] = multiplier;
        }

        /// <summary>
        /// Retorna o multiplicador de efetividade do tipo de ataque contra o tipo de defesa.
        /// Retorna 1.0 caso não haja regra específica.
        /// </summary>
        public double GetEffectiveness(ElementType attack, ElementType defense)
        {
            return effectivenessTable.TryGetValue((attack, defense), out var value)
                ? value
                : DefaultMultiplier;
        }

        /// <summary>
        /// Calcula o dano final considerando a efetividade de tipo.
        /// </summary>
        public double CalculateDamage(double baseDamage, ElementType attack, ElementType defense)
        {
            return baseDamage * GetEffectiveness(attack, defense);
        }
    }
}