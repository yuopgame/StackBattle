using System;
using StackBattle.Unit.Units;

namespace StackBattle.Unit
{
    class UnitGenerator
    {
        public static IUnit generateUnit(Type unitType)
        {
            switch (unitType.Name)
            {
                case "InfantryUnit":
                    return generateInfantryMan();
                case "HeavyUnit":
                    return generateHeavyUnit();
                case "ArcherUnit":
                    return generateArcherUnit();
                case "ClericUnit":
                    return generateClericUnit();
                case "WalkCityUnit":
                    return generateWalkCityUnit();
                case "MageUnit":
                    return generateMageUnit();
                default:
                    return generateInfantryMan();
            }
        }

        private static IUnit generateMageUnit()
        {
            return new MageUnit();
        }

        private static IUnit generateWalkCityUnit()
        {
            return new WalkCityUnit();
        }

        private static IUnit generateClericUnit()
        {
            return new ClericUnit();
        }

        private static IUnit generateArcherUnit()
        {
            return new ArcherUnit();
        }

        private static IUnit generateHeavyUnit()
        {
            return new HeavyUnitProxy();
        }
        private static InfantryUnit generateInfantryMan()
        {
            return new InfantryUnit();
        }
    }
}
