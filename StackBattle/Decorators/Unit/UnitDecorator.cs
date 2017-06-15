using System;


namespace StackBattle.Decorators.Unit
{
    abstract class UnitDecorator
    {
        protected UnitDecorator _component;

        public bool SetComponent(UnitDecorator prevDecorator)
        {
            UnitDecorator checkingComponent = prevDecorator;
            bool found = false;

            while (!found && checkingComponent != null)
            {
                found = String.Equals(checkingComponent.ToString(), this.ToString());
                checkingComponent = checkingComponent.GetComponent();
            }

            if (!found)
            {
                this._component = prevDecorator;
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public UnitDecorator GetComponent()
        {
            return this._component;
        }

        public virtual UnitBonuses getBonuses(UnitBonuses bonuses = null)
        {
            return bonuses;
        }
    }
}
