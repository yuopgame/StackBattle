using StackBattle.Armies;
using StackBattle.Engine;
using StackBattle.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackBattle.Commands
{
    class TurnCommand : Command
    {
        IArmy army1, army2;
        List<IUnit> army1_units_before = new List<IUnit>(),
                    army2_units_before = new List<IUnit>(),
                    army1_units_after = new List<IUnit>(),
                    army2_units_after = new List<IUnit>();
        public TurnCommand(IArmy _army1, IArmy _army2)
        {
            army1 = _army1;
            this.fillUnitsFromArmies(ref army1_units_before, army1);

            army2 = _army2;
            this.fillUnitsFromArmies(ref army2_units_before, army2);
        }

        public override void Execute()
        {
            int armyHitsFirst = GameEngine.Instance.Random.Next(0, 2);

            IArmy turnArmy1 = armyHitsFirst == 0 ? army1 : army2;
            IArmy turnArmy2 = armyHitsFirst == 0 ? army2 : army1;

            turnArmy1.Turn(turnArmy2);
            turnArmy2.Turn(turnArmy1);
            turnArmy1.SpecialAbilities(turnArmy2);
            turnArmy2.SpecialAbilities(turnArmy1);
            turnArmy1.AfterTurn();
            turnArmy2.AfterTurn();

            this.fillUnitsFromArmies(ref army1_units_after, army1);
            this.fillUnitsFromArmies(ref army2_units_after, army2);
        }

        public override void Undo()
        {
            army1.Units = army1_units_before;
            army2.Units = army2_units_before;
        }

        public override void Redo()
        {
            army1.Units = army1_units_after;
            army2.Units = army2_units_after;
        }

        private void fillUnitsFromArmies(ref List<IUnit> units, IArmy fromArmy)
        {
            foreach (var unit in fromArmy.Units)
            {
                IUnit newUnit = UnitGenerator.generateUnit(unit.GetType());
                newUnit.Health = unit.Health;
                newUnit.Defence = unit.Defence;
                newUnit.Attack = unit.Attack;
                units.Add(newUnit);
            }
        }
    }
}
