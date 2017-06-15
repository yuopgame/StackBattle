using System;
using StackBattle.Engine;

namespace StackBattle.Decorators.Unit
{
    class UnitDecoratorFactory
    {
        public static UnitDecorator getRandomDecorator()
        {
            int rndNum = GameEngine.Instance.Random.Next(1, 5);
            switch (rndNum)
            {
                case 1:
                    return new UnitHelmetDecorator();
                case 2:
                    return new UnitHorseDecorator();
                case 3:
                    return new UnitPikeDecorator();
                case 4:
                    return new UnitShieldDecorator();
                default:
                    return null;
            }
        }
    }
}
