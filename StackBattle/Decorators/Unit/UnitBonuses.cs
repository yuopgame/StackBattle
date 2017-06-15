using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackBattle.Decorators.Unit
{
    class UnitBonuses
    {
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int MaxHealth { get; set; }

        public UnitBonuses()
        {
            this.Attack = 0;
            this.Defence = 0;
            this.MaxHealth = 0;
        }
    }
}
