using StackBattle.SpecialAbilities;
using System;

namespace StackBattle.Unit.Units
{
    class HeavyUnitProxy : AbstractUnit, IDressable
    {
        private HeavyUnit heavyUnit;
        public new const int cost = 10;

        public HeavyUnitProxy()
        {
            this.heavyUnit = new HeavyUnit();
        }

        public int Health
        {
            get { return heavyUnit.Health; }
            set { heavyUnit.Health = value; }
        }

        public int Defence
        {
            get { return heavyUnit.Defence; }
            set { heavyUnit.Defence = value; }
        }

        public int Attack
        {
            get { return heavyUnit.Attack; }
            set { heavyUnit.Attack = value; }
        }

        public bool isDead()
        {
            return heavyUnit.isDead();
        }

        public void Turn(IUnit attackedUnit)
        {
            if (attackedUnit == null) return;

            string logText = String.Format("{0} совершает ход, атакуя {1}", heavyUnit.ToString(), attackedUnit.ToString());
            this.log(logText);

            heavyUnit.Turn(attackedUnit);
        }

        public void TakeDamage(int damage, IUnit attacker)
        {
            string logText = String.Format("{0} получает урон от {1}", heavyUnit.ToString(), attacker.ToString());
            this.log(logText);
            heavyUnit.TakeDamage(damage, attacker);
        }

        public IUnit GetTarget(Armies.IArmy fromArmy)
        {
            return heavyUnit.GetTarget(fromArmy);
        }

        private void log(string text)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter("heavyUnit.log", true))
            {
                file.WriteLine(text);
            }  
        }

        public override string ToString()
        {
            return heavyUnit.ToString();
        }


        public ConsoleColor LogColor
        {
            get { return heavyUnit.LogColor; }
            set { heavyUnit.LogColor = value; }
        }

        public Decorators.Unit.UnitDecorator Dress
        {
            get { return this.heavyUnit.Dress; }
            set { this.heavyUnit.Dress = value; }
        }

        public bool Wear(Decorators.Unit.UnitDecorator item)
        {
            return this.heavyUnit.Wear(item);
        }
    }
}
