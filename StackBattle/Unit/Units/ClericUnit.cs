using System;
using StackBattle.SpecialAbilities;
using StackBattle.Armies;
using System.Collections.Generic;
using StackBattle.Engine;
using StackBattle.Logger;

namespace StackBattle.Unit.Units
{
    class ClericUnit : AbstractUnit, ISpecialAbility, IClonable
    {
        public new const int cost = 12;

        public ClericUnit()
        {
            this.Health = 10;
            this.Attack = 3;
            this.Defence = 1;
            this.LogColor = ConsoleColor.Yellow;
        }

        protected int healRange = 2;
        public int SpecialAbilityRange
        {
            get { return this.healRange; }
            set { this.healRange = value < 0 ? this.healRange : value; }
        }
        
        protected int healPower = 3;
        public int SpecialAbilityPower
        {
            get { return this.healPower; }
            set { this.healPower = value; }
        }

        protected int specialAbilityChance = 100;
        public int SpecialAbilityChance
        {
            get { return this.specialAbilityChance; }
            set { this.specialAbilityChance = value >= 0 ? value : 0; }
        }

        public override string ToString()
        {
            return "ClericUnit";
        }

        public bool SpecialAbilityProc() { return true; }

        public void DoSpecialAbility(IArmy friendlyArmy, IArmy enemiesArmy)
        {
            List<IUnit> availableRange = friendlyArmy.FightStrategy.getSpecialAbilityTargets(friendlyArmy, enemiesArmy, this);
            if (availableRange.Count <= 0) return;

            List<IHealable> allies = new List<IHealable>();
            foreach (var unit in availableRange)
            {
                if (friendlyArmy.Units.Contains(unit) && unit is IHealable)
                {
                    IHealable hUnit = unit as IHealable;
                    if (hUnit.Health != hUnit.MaxHealth)
                    {
                        allies.Add(hUnit);
                    }
                }
            }

            if (allies.Count > 0)
            {
                int healIndex = GameEngine.Instance.Random.Next(0, allies.Count);
                IHealable healableUnit = allies[healIndex];

                int currentHealth = healableUnit.Health;
                int maxHealth = healableUnit.MaxHealth;

                int newHealth = currentHealth + this.SpecialAbilityPower < maxHealth
                    ? currentHealth + this.SpecialAbilityPower : maxHealth;

                healableUnit.Health = newHealth;

                GameLogger.Instance.Log(
                    String.Format("[{0}] лечит [{1}] на {2} единиц ({3} -> {4})",
                        this.ToString(), healableUnit.ToString(), this.SpecialAbilityPower, currentHealth, newHealth),
                    GameLogger.LOG_LEVEL__SPECIAL_ABILITY
                );
            }
        }

        public IUnit Clone()
        {
            return (IUnit)this.MemberwiseClone();
        }
    }
}
