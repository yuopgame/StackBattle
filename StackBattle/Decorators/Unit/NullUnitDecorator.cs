using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackBattle.Decorators.Unit
{
    class NullUnitDecorator: UnitDecorator
    {
        public override UnitBonuses getBonuses(UnitBonuses bonuses)
        {
            if (bonuses == null)
            {
                bonuses = new UnitBonuses();
            }
            return bonuses;
        }
    }
}
