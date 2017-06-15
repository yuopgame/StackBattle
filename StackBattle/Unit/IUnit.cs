using System;
using StackBattle.Armies;
using StackBattle.Observers;

namespace StackBattle.Unit
{
    public interface IUnit : IObservable
    {
        int Health { get; set; }
        int Defence { get; set; }
        int Attack { get; set; }

        ConsoleColor LogColor { get; set; }

        bool isDead();
        void Turn(IUnit attackedUnit);
        void TakeDamage(int damage, IUnit attacker);
        IUnit GetTarget(IArmy fromArmy);
    }
}
