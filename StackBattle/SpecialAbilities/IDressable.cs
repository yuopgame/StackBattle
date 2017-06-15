using StackBattle.Decorators.Unit;
using StackBattle.Unit;
using System;


namespace StackBattle.SpecialAbilities
{
    interface IDressable : IUnit
    {
        UnitDecorator Dress { get; set; }
        bool Wear(UnitDecorator item);
    }
}
