using System;
using StackBattle.SpecialAbilities;
using StackBattle.Armies;
using System.Collections.Generic;
using StackBattle.Engine;

namespace StackBattle.Unit.Units
{
    class ArcherUnit : AbstractUnit, ISpecialAbility, IHealable, IClonable
    {
        public new const int cost = 7;

        public ArcherUnit()
        {
            this.Health = 7;
            this.Attack = 3;
            this.Defence = 2;
            this.LogColor = ConsoleColor.DarkGray;
        }

        protected int attackRange = 4;
        public int SpecialAbilityRange
        {
            get { return this.attackRange; }
            set { this.attackRange = value < 0 ? this.attackRange : value; }
        }
        
        protected int rangedAttackDmg = 5;
        public int SpecialAbilityPower
        {
            get { return this.rangedAttackDmg;  }
            set { this.rangedAttackDmg = value; }
        }
        
        protected int specialAbilityChance = 100;
        public int SpecialAbilityChance
        {
            get { return this.specialAbilityChance; }
            set { this.specialAbilityChance = value >= 0 ? value : 0; }
        }

        protected int maxHealth = 7;
        public int MaxHealth
        {
            get { return this.maxHealth; }
        }

        public override string ToString()
        {
            return "ArcherUnit";
        }

        public bool SpecialAbilityProc() { return true; }

        public void DoSpecialAbility(IArmy friendlyArmy, IArmy enemiesArmy)
        {
            if (friendlyArmy.Units.IndexOf(this) == 0)
            {
                return;
            }
                
            List<IUnit> availableRange = friendlyArmy.FightStrategy.getSpecialAbilityTargets(friendlyArmy, enemiesArmy, this);
            if (availableRange.Count <= 0) return;

            List<IUnit> enemies = new List<IUnit>();
            foreach (var unit in availableRange)
            {
                if (enemiesArmy.Units.Contains(unit))
                {
                    enemies.Add(unit);
                }
            }

            if (enemies.Count > 0)
            {
                int hitIndex = GameEngine.Instance.Random.Next(0, enemies.Count);
                enemies[hitIndex].TakeDamage(this.SpecialAbilityPower, this);
            }
        }

        public IUnit Clone()
        {
            return (IUnit)this.MemberwiseClone();
        }
    }
}
