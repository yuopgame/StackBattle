using System;
using StackBattle.SpecialAbilities;
using StackBattle.Armies;
using StackBattle.Engine;
using System.Collections.Generic;
using StackBattle.Logger;

namespace StackBattle.Unit.Units
{
    class MageUnit : AbstractUnit, IUnit, IHealable, ISpecialAbility
    {
        public new const int cost = 9;

        protected int maxHealth = 10;
        public int MaxHealth
        {
            get { return this.maxHealth; }
        }

        protected int abilityRange = 1;
        public int SpecialAbilityRange
        {
            get { return this.abilityRange; }
            set { this.abilityRange = value < 0 ? this.abilityRange : value; }
        }

        protected int abilityPower = 0;
        public int SpecialAbilityPower
        {
            get { return this.abilityPower; }
            set { this.abilityPower = value; }
        }

        protected int specialAbilityChance = 5;
        public int SpecialAbilityChance
        {
            get { return this.specialAbilityChance; }
            set { this.specialAbilityChance = value >= 0 ? value : 0; }
        }

        public MageUnit()
        {
            this.Health = 6;
            this.Attack = 0;
            this.Defence = 1;
            this.LogColor = ConsoleColor.Blue;
        }

        public bool SpecialAbilityProc()
        {
            int rndNum = GameEngine.Instance.Random.Next(0, 101);
            
            return rndNum <= this.SpecialAbilityChance;
        }

        public void DoSpecialAbility(IArmy friendlyArmy, IArmy enemiesArmy)
        {
            List<IUnit> availableRange = friendlyArmy.FightStrategy.getSpecialAbilityTargets(friendlyArmy, enemiesArmy, this);
            if (availableRange.Count <= 0) return;

            List<IUnit> allies = new List<IUnit>();
            foreach (var unit in availableRange)
            {
                if (friendlyArmy.Units.Contains(unit) && unit is IClonable)
                {
                    allies.Add(unit);
                }
            }

            if (allies.Count > 0)
            {
                int cloneIndex = GameEngine.Instance.Random.Next(0, allies.Count);
                IClonable clonableUnit = allies[cloneIndex] as IClonable;

                IUnit clone = clonableUnit.Clone();
                friendlyArmy.Units.Add(clone);

                GameLogger.Instance.Log(
                    String.Format("[{0}] копирует [{1}] c параметрами [H:{2}, A:{3}, D:{4}]",
                        this.ToString(), clone.ToString(), clone.Health, clone.Attack, clone.Defence),
                    GameLogger.LOG_LEVEL__SPECIAL_ABILITY
                );
            }
        }

        public override string ToString()
        {
            return "MageUnit";
        }
    }
}
