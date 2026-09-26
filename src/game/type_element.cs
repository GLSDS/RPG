using System;
using System.Collections.Generic;

namespace type_element
{
    public enum TypeElement
    {
        Normal,
        Fire,
        Water,
        Plant,      // equivalente a "Ground"
        Air,        // equivalente a "Flying"
        Lightning,  // equivalente a "Electric"
        Ice,
        Light,
        Dark,
        Fight,      // equivalente a "Fighting"
        Bug,
        Poison
    }

    public class ElementTypeEffectiveness
{
    private readonly Dictionary<(TypeElement, TypeElement), double> effectivenessTable;
    private const double DefaultMultiplier = 1.0;

    public ElementTypeEffectiveness()
    {
        effectivenessTable = new Dictionary<(TypeElement, TypeElement), double>();

        // Normal
        AddEffectiveness(TypeElement.Normal, TypeElement.Fight, 0.5);
        AddEffectiveness(TypeElement.Normal, TypeElement.Bug, 2.0);

        // Fire
        AddEffectiveness(TypeElement.Fire, TypeElement.Bug, 2.0);
        AddEffectiveness(TypeElement.Fire, TypeElement.Water, 0.5);
        AddEffectiveness(TypeElement.Fire, TypeElement.Plant, 0.5);
        AddEffectiveness(TypeElement.Fire, TypeElement.Normal, 2.0);

        // Water
        AddEffectiveness(TypeElement.Water, TypeElement.Fire, 2.0);
        AddEffectiveness(TypeElement.Water, TypeElement.Plant, 2.0);
        AddEffectiveness(TypeElement.Water, TypeElement.Lightning, 0.5);

        // Plant
        AddEffectiveness(TypeElement.Plant, TypeElement.Lightning, 2.0);
        AddEffectiveness(TypeElement.Plant, TypeElement.Air, 1.5);
        AddEffectiveness(TypeElement.Plant, TypeElement.Fire, 2.0);

        // Air
        AddEffectiveness(TypeElement.Air, TypeElement.Plant, 0.0);
        AddEffectiveness(TypeElement.Air, TypeElement.Fight, 2.0);
        AddEffectiveness(TypeElement.Air, TypeElement.Bug, 2.0);

        // Lightning
        AddEffectiveness(TypeElement.Lightning, TypeElement.Water, 2.0);
        AddEffectiveness(TypeElement.Lightning, TypeElement.Air, 2.0);
        AddEffectiveness(TypeElement.Lightning, TypeElement.Plant, 0.5);
        AddEffectiveness(TypeElement.Lightning, TypeElement.Normal, 1.5);

        // Ice
        AddEffectiveness(TypeElement.Ice, TypeElement.Air, 2.0);
        AddEffectiveness(TypeElement.Ice, TypeElement.Plant, 2.0);
        AddEffectiveness(TypeElement.Ice, TypeElement.Fire, 0.5);

        // Light
        AddEffectiveness(TypeElement.Light, TypeElement.Dark, 2.0);
        AddEffectiveness(TypeElement.Light, TypeElement.Poison, 2.0);
        AddEffectiveness(TypeElement.Light, TypeElement.Bug, 0.5);

        // Dark
        AddEffectiveness(TypeElement.Dark, TypeElement.Light, 2.0);
        AddEffectiveness(TypeElement.Dark, TypeElement.Fight, 0.5);
        AddEffectiveness(TypeElement.Dark, TypeElement.Bug, 2.0);
        AddEffectiveness(TypeElement.Dark, TypeElement.Air, 2.0);

        // Fight
        AddEffectiveness(TypeElement.Fight, TypeElement.Dark, 2.0);
        AddEffectiveness(TypeElement.Fight, TypeElement.Ice, 2.0);
        AddEffectiveness(TypeElement.Fight, TypeElement.Poison, 0.5);
        AddEffectiveness(TypeElement.Fight, TypeElement.Normal, 1.5);

        // Bug
        AddEffectiveness(TypeElement.Bug, TypeElement.Dark, 2.0);
        AddEffectiveness(TypeElement.Bug, TypeElement.Fire, 0.5);
        AddEffectiveness(TypeElement.Bug, TypeElement.Fight, 0.5);
        AddEffectiveness(TypeElement.Bug, TypeElement.Normal, 2.0);

        // Poison
        AddEffectiveness(TypeElement.Poison, TypeElement.Plant, 0.5);
        AddEffectiveness(TypeElement.Poison, TypeElement.Fire, 0.5);
        AddEffectiveness(TypeElement.Poison, TypeElement.Light, 2.0);
    }

    private void AddEffectiveness(TypeElement attack, TypeElement defense, double multiplier)
        => effectivenessTable[(attack, defense)] = multiplier;

    public double GetEffectiveness(TypeElement attack, TypeElement defense)
        => effectivenessTable.TryGetValue((attack, defense), out var v) ? v : DefaultMultiplier;

    public double CalculateDamage(double baseDamage, TypeElement attack, TypeElement defense)
        => baseDamage * GetEffectiveness(attack, defense);
    }
}