using System;
using System.Collections.Generic;
using StackBattle.SpecialAbilities;
using StackBattle.Engine;
using StackBattle.Decorators.Unit;
using StackBattle.Logger;

namespace StackBattle.Unit.Units
{
    class InfantryUnit : AbstractUnit, IHealable, IClonable, ISpecialAbility
    {
        public new const int cost = 5;

        protected int maxHealth = 10;
        public int MaxHealth
        {
            get { return this.maxHealth; }
        }

        public InfantryUnit()
        {
            this.Health = 10;
            this.Attack = 7;
            this.Defence = 4;
            this.LogColor = ConsoleColor.Cyan;
        }

        public override string ToString()
        {
            return "InfantryUnit";
        }

        public IUnit Clone()
        {
            return (IUnit)this.MemberwiseClone();
        }

        protected int specialAbilityRange = 1;
        public int SpecialAbilityRange
        {
            get { return this.specialAbilityRange; }
            set { this.specialAbilityRange = value < 0 ? this.specialAbilityRange : value; }
        }

        protected int specialAbilityPower = 0;
        public int SpecialAbilityPower
        {
            get { return this.specialAbilityPower; }
            set { this.specialAbilityPower = value; }
        }

        protected int specialAbilityChance = 5;
        public int SpecialAbilityChance
        {
            get { return this.specialAbilityChance; }
            set { this.specialAbilityChance = value >= 0 ? value : 0; }
        }

        public void DoSpecialAbility(Armies.IArmy friendlyArmy, Armies.IArmy enemiesArmy)
        {
            List<IUnit> availableRange = friendlyArmy.FightStrategy.getSpecialAbilityTargets(friendlyArmy, enemiesArmy, this);
            if (availableRange.Count <= 0) return;

            List<IUnit> allies = new List<IUnit>();
            foreach (var unit in availableRange)
            {
                if (friendlyArmy.Units.Contains(unit) && unit is IDressable)
                {
                    allies.Add(unit);
                }
            }

            if (allies.Count > 0)
            {
                int dressIndex = GameEngine.Instance.Random.Next(0, allies.Count);
                IDressable dressableUnit = allies[dressIndex] as IDressable;

                UnitDecorator dress = UnitDecoratorFactory.getRandomDecorator();

                if (dressableUnit.Wear(dress))
                {
                    GameLogger.Instance.Log(
                        String.Format("[{0}] усиливает [{1}], добавляя {2} к его снаряжению",
                            this.ToString(), dressableUnit.ToString(), dress.ToString()),
                        GameLogger.LOG_LEVEL__SPECIAL_ABILITY
                    );
                }
            }
        }

        public bool SpecialAbilityProc()
        {
            int rndNum = GameEngine.Instance.Random.Next(0, 101);

            return rndNum <= this.SpecialAbilityChance;
        }
    }
}
