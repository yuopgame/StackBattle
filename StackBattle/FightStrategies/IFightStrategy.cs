using StackBattle.Armies;
using StackBattle.SpecialAbilities;
using StackBattle.Unit;
using System;
using System.Collections.Generic;

namespace StackBattle.FightStrategies
{
    public interface IFightStrategy
    {
        List<IUnit> getMainTurnUnits(IArmy army);
        IUnit getTarget(IArmy friendlyArmy, IArmy enemiesArmy, IUnit unit);
        List<IUnit> getSpecialAbilityTargets(IArmy friendlyArmy, IArmy enemiesArmy, ISpecialAbility unit);
    }
}
