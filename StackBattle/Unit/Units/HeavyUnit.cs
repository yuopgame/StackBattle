using StackBattle.Decorators.Unit;
using StackBattle.SpecialAbilities;
using System;

namespace StackBattle.Unit.Units
{
    class HeavyUnit : AbstractUnit, IClonable, IDressable
    {
        public new const int cost = 10;

        protected UnitDecorator dress = new NullUnitDecorator();
        public UnitDecorator Dress
        {
            get { return this.dress; }
            set { this.dress = value; }
        }

        public virtual int Attack
        {
            get { return this.attack + this.Dress.getBonuses().Attack; }
            set { this.attack = value; }
        }

        public virtual int Defence
        {
            get { return this.defence + this.Dress.getBonuses().Defence; }
            set { this.defence = value; }
        }

        public HeavyUnit()
        {
            this.Health = 15;
            this.Attack = 4;
            this.Defence = 6;
            this.LogColor = ConsoleColor.Red;
        }

        public override string ToString()
        {
            return "HeavyUnit";
        }

        public IUnit Clone()
        {
            return (IUnit)this.MemberwiseClone();
        }

        public bool Wear(UnitDecorator item)
        {
            UnitDecorator unitBonuses = this.Dress;
            if (item.SetComponent(unitBonuses))
            {
                this.Dress = item;
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
