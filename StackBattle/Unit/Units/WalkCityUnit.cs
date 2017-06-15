using SpecialUnits;
using StackBattle.Logger;
using StackBattle.SpecialAbilities;
using System;


namespace StackBattle.Unit.Units
{
    class WalkCityUnit : AbstractUnit, IClonable
    {
        protected GulyayGorod adaptingUnit;
        public GulyayGorod AdaptingUnit { get { return this.adaptingUnit; } }

        public WalkCityUnit()
        {
            this.adaptingUnit = new GulyayGorod(25, 1, WalkCityUnit.cost);
            this.LogColor = ConsoleColor.Green;
        }

        public new const int cost = 10;

        public override int Health 
        {
            get { return this.AdaptingUnit.GetCurrentHealth(); }
            set 
            {
                int diff = this.AdaptingUnit.GetCurrentHealth() - value;
                if (this.AdaptingUnit.GetCurrentHealth() - diff > this.AdaptingUnit.GetHealth())
                {
                    diff = this.AdaptingUnit.GetCurrentHealth() - this.AdaptingUnit.GetHealth();
                }
                this.AdaptingUnit.TakeDamage(diff);
            } 
        }

        public override int Defence
        {
            get { return this.AdaptingUnit.GetDefence(); }
            set { }
        }

        public IUnit Clone()
        {
            return (IUnit)this.MemberwiseClone();
        }

        public override int Attack
        {
            get { return this.AdaptingUnit.GetStrength(); }
            set { }
        }

        public override bool isDead()
        {
            return this.AdaptingUnit.GetCurrentHealth() <= 0;
        }

        public override void Turn(IUnit attackedUnit) { }

        public override void TakeDamage(int damage, IUnit attacker)
        {
            int takingDamage = this.calculateTakingDamage(damage);

            GameLogger.Instance.Log(
                String.Format("[{0}] наносит {1} урона [{2}] ({3} -> {4})", attacker.ToString(), takingDamage, this.ToString(),
                    this.Health, this.Health - takingDamage),
                GameLogger.LOG_LEVEL__ATTACK
            );

            this.AdaptingUnit.TakeDamage(takingDamage);
            if (this.isDead())
            {
                GameLogger.Instance.Log(String.Format("[{0}] погибает", this.ToString()), GameLogger.LOG_LEVEL__DEATH);
            }
            
        }

        public override string ToString()
        {
            return "WalkCityUnit";
        }
    }
}
