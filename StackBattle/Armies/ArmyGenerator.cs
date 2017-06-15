using System;
using System.Linq;
using System.Collections.Generic;
using StackBattle.Unit;
using StackBattle.Engine;

namespace StackBattle.Armies
{
    public class ArmyGenerator
    {
        public static IArmy GenerateArmy(int cost)
        {
            int sumCost = 0;
            Dictionary<Type, double> unitCosts;
            Dictionary<Type, double> unitWeights = ArmyGenerator.getUnitCosts(out sumCost, out unitCosts);
            
            
            Army army = new Army();
            bool noUnits = false;
            Random random = GameEngine.Instance.Random;
            Dictionary<Type, double> availableUnits = new Dictionary<Type, double>();

            while (cost > 0 && !noUnits)
            {
                noUnits = true;
                int rnd = random.Next(1, sumCost + 1);
                availableUnits.Clear();

                foreach (KeyValuePair<Type, double> unit in unitWeights)
                {
                    int unitCost = (int)unitCosts[unit.Key];
                    if (cost - unitCost >= 0)
                    {
                        availableUnits.Add(unit.Key, unit.Value);
                        if (rnd <= unit.Value)
                        {
                            noUnits = false;
                            army.AddUnit(UnitGenerator.generateUnit(unit.Key));
                            cost -= unitCost;
                            break;
                        }
                    }
                }
                if (noUnits && availableUnits.Count > 0)
                {
                    List<Type> keys = new List<Type>(availableUnits.Keys);
                    int randomIndex = random.Next(keys.Count);
                    army.AddUnit(UnitGenerator.generateUnit(keys[randomIndex]));
                    cost -= (int)availableUnits[keys[randomIndex]];
                    noUnits = false;
                }
            }

            return army;
        }

        private static Dictionary<Type, double> getUnitCosts(out int sumCost, out Dictionary<Type, double> unitCosts)
        {
            var unitType = typeof(IUnit);
            var unitTypes = AppDomain.CurrentDomain.GetAssemblies()
                                .SelectMany(s => s.GetTypes())
                                .Where(p => unitType.IsAssignableFrom(p) && !p.IsInterface
                                    && !p.IsAbstract);

            unitCosts = new Dictionary<Type, double>();
            sumCost = 0;
            double mulCost = 1;

            foreach (var type in unitTypes)
            {
                int unitCost = (int)type.GetField("cost").GetValue(null);
                mulCost *= unitCost;
                unitCosts.Add(type, unitCost);
            }

            sumCost = 0;

            Dictionary<Type, double> finalCosts = new Dictionary<Type, double>();
            foreach (KeyValuePair<Type, double> unit in unitCosts)
            {
                int weight = (int)Math.Round( mulCost / unit.Value );
                sumCost += weight;
                finalCosts.Add(unit.Key, sumCost);
            }

            return finalCosts;
        }

    }
}
