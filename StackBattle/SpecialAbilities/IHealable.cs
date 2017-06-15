using System;
using StackBattle.Unit;

namespace StackBattle.SpecialAbilities
{
    interface IHealable: IUnit
    {
        int MaxHealth { get; }
    }
}
