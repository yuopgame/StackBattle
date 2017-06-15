using StackBattle.Unit;
using StackBattle.Unit.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using StackBattle.SpecialAbilities;
using StackBattle.Logger;
using StackBattle.FightStrategies;

namespace StackBattle.Armies
{
    class Army : IArmy
    {
        public List<IUnit> Units { get; set; }
        public IFightStrategy FightStrategy { get; set; }

        public Army()
        {
            this.Units = new List<IUnit>();
            this.FightStrategy = new StackFightStrategy();
        }

        public void AddUnit(IUnit unit)
        {
            this.Units.Add(unit);
        }

        public bool IsDefeated() 
        {
            return this.Units.Count <= 0;
        }
        
        public void Turn(IArmy enemiesArmy)
        {
            List<IUnit> unitsToAttack = this.FightStrategy.getMainTurnUnits(this);
            foreach (var unit in unitsToAttack)
            {
                if (!unit.isDead())
                {
                    unit.Turn(this.FightStrategy.getTarget(this, enemiesArmy, unit));
                }
            }
        }

        public void AfterTurn()
        {
            for (int i = 0; i < this.Units.Count; i++)
            {
                if (this.Units[i].isDead())
                {
                    this.RemoveUnit(i);
                }
            }
        }

        public void SpecialAbilities(IArmy enemiesArmy)
        {
            int unitsCount = this.Units.Count();
            for (int i = 0; i < unitsCount; i++) 
            {
                IUnit unit = this.Units[i];
                if (!unit.isDead())
                {
                    ISpecialAbility saUnit = unit as ISpecialAbility;
                    if (saUnit != null && saUnit.SpecialAbilityProc())
                    {
                        saUnit.DoSpecialAbility(this, enemiesArmy);
                    }
                }
            }
        }

        private void RemoveUnit(int at = 0)
        {
            this.Units.RemoveAt(at);
        }

        public IUnit GetUnit(int at = 0) 
        {
            return this.Units[at];
        }

        public void Log()
        {
            IUnit[] units = this.Units.ToArray();
            foreach (var unit in units)
            {
                GameLogger.Instance.Log(String.Format("[{0}] ", unit.ToString()), unit.LogColor);
            }
            Console.WriteLine();
        }
    }
}
