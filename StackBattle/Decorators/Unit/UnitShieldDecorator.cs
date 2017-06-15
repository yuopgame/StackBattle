using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackBattle.Decorators.Unit
{
    class UnitShieldDecorator : UnitDecorator
    {
        public override UnitBonuses getBonuses(UnitBonuses bonuses = null)
        {
            if (bonuses == null)
            {
                bonuses = new UnitBonuses();
            }
            bonuses.Defence += 2;
            return this._component.getBonuses(bonuses);
        }

        public override string ToString()
        {
            return "Щит";
        }
    }
}
