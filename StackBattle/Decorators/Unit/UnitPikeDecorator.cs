using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackBattle.Decorators.Unit
{
    class UnitPikeDecorator: UnitDecorator
    {
        public override UnitBonuses getBonuses(UnitBonuses bonuses = null)
        {
            if (bonuses == null)
            {
                bonuses = new UnitBonuses();
            }
            bonuses.Attack += 3;
            return this._component.getBonuses(bonuses);
        }

        public override string ToString()
        {
            return "Длинный меч";
        }
    }
}
