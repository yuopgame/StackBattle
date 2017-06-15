using StackBattle.Armies;
using StackBattle.SpecialAbilities;
using StackBattle.Unit;
using System;
using System.Collections.Generic;

namespace StackBattle.FightStrategies
{
    class WallFightStrategy : IFightStrategy
    {
        public List<IUnit> getMainTurnUnits(IArmy army)
        {
            return army.Units;
        }

        public IUnit getTarget(IArmy friendlyArmy, IArmy enemiesArmy, IUnit unit)
        {
            int unitPosition = friendlyArmy.Units.IndexOf(unit);
            if (enemiesArmy.Units.Count > unitPosition)
            {
                return enemiesArmy.Units[unitPosition];
            }

            return null;
        }

        public List<IUnit> getSpecialAbilityTargets(IArmy friendlyArmy, IArmy enemiesArmy, ISpecialAbility unit)
        {
            List<IUnit> friendlyUnits = friendlyArmy.Units;
            int position = friendlyUnits.IndexOf(unit);

            int i = 0;
            List<IUnit> availableRange = new List<IUnit>();

            foreach (var armyUnit in friendlyUnits)
            {
                if (!armyUnit.isDead() && Math.Abs(position - i) <= unit.SpecialAbilityRange && i != position)
                {
                    availableRange.Add(armyUnit);
                }
                i++;
            }

            i = 0;
            foreach (var armyUnit in enemiesArmy.Units)
            {
                if (!armyUnit.isDead() && Math.Abs(position - i) <= unit.SpecialAbilityRange - 1)
                {
                    availableRange.Add(armyUnit);
                }
                i++;
            }

            return availableRange;
        }
    }
}
