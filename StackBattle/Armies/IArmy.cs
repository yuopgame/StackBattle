using System;
using System.Collections.Generic;
using StackBattle.Unit;
using StackBattle.FightStrategies;

namespace StackBattle.Armies
{
    public interface IArmy
    {
        IFightStrategy FightStrategy { get; set; }
        List<IUnit> Units { get; set; }
        IUnit GetUnit(int at = 0);
        bool IsDefeated();
        void Log();
        void Turn(IArmy attackedArmy);
        void AfterTurn();
        void SpecialAbilities(IArmy enemiesArmy);
    }
}
