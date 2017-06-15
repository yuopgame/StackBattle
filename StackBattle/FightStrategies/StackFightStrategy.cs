using StackBattle.Armies;
using StackBattle.SpecialAbilities;
using StackBattle.Unit;
using System;
using System.Collections.Generic;

namespace StackBattle.FightStrategies
{
    class StackFightStrategy : IFightStrategy
    {
        public List<IUnit> getMainTurnUnits(IArmy army)
        {
            List<IUnit> units = new List<IUnit>();
            units.Add(army.Units[0]);

            return units;
        }

        public IUnit getTarget(IArmy friendlyArmy, IArmy enemiesArmy, IUnit unit)
        {
            return enemiesArmy.Units[0];
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

            int range = unit.SpecialAbilityRange - position;
            i = 0;

            while (i < range && i < enemiesArmy.Units.Count)
            {
                IUnit armyUnit = enemiesArmy.Units[i];
                if (!armyUnit.isDead())
                {
                    availableRange.Add(armyUnit);
                }
                i++;
            }

            return availableRange;
        }
    }
}
