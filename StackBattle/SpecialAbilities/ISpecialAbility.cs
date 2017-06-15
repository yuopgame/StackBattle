using System;
using StackBattle.Armies;
using StackBattle.Unit;
using System.Collections.Generic;

namespace StackBattle.SpecialAbilities
{
    public interface ISpecialAbility : IUnit
    {
        int SpecialAbilityRange { get; set; }
        int SpecialAbilityPower { get; set; }
        int SpecialAbilityChance { get; set; }
        void DoSpecialAbility(IArmy friendlyArmy, IArmy enemiesArmy);
        bool SpecialAbilityProc();
    }
}
