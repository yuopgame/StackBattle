using StackBattle.Unit;
using System;

namespace StackBattle.SpecialAbilities
{
    interface IClonable : IUnit
    {
        IUnit Clone();
    }
}
