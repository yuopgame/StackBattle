using StackBattle.Armies;
using StackBattle.SpecialAbilities;
using StackBattle.Unit;
using System;
using System.Collections.Generic;

namespace StackBattle.FightStrategies
{
    class ThreeByThreeFightStrategy : IFightStrategy
    {
        const int N = 3;

        public List<IUnit> getMainTurnUnits(IArmy army)
        {
            int rangeCount = army.Units.Count < N ? army.Units.Count : N;
            
            return army.Units.GetRange(0, rangeCount);
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
            
            int colNum = position / N,
                rowNum = position % N;

            int i = 0;
            List<IUnit> availableRange = new List<IUnit>();

            foreach (var armyUnit in friendlyUnits)
            {
                int unitColNum = i / N,
                    unitRowNum = i % N;
                if (!armyUnit.isDead() && i != position 
                    && Math.Abs(unitColNum - colNum) + Math.Abs(unitRowNum - rowNum) <= unit.SpecialAbilityRange)
                {
                    availableRange.Add(armyUnit);
                }
                i++;
            }

            i = 0;
            foreach (var armyUnit in enemiesArmy.Units)
            {
                int unitColNum = i / N,
                    unitRowNum = i % N;
                if (!armyUnit.isDead() && i != position
                    && Math.Abs(unitColNum - colNum) + Math.Abs(unitRowNum - rowNum) + colNum + 1 <= unit.SpecialAbilityRange)
                {
                    availableRange.Add(armyUnit);
                }
                i++;
            }

            return availableRange;
        }
    }
}
