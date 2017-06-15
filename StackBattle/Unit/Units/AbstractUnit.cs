using System;
using StackBattle.Logger;
using StackBattle.Armies;
using StackBattle.Observers;
using System.Collections.Generic;

namespace StackBattle.Unit.Units
{
    abstract class AbstractUnit : IUnit
    {
        protected List<IObserver> observers = new List<IObserver>();
        public List<IObserver> Observers 
        { 
            get { return this.observers; }
            private set { this.observers = value; }
        }

        public const int cost = 5;

        protected ConsoleColor logColor = ConsoleColor.White;
        public virtual ConsoleColor LogColor 
        { 
            get { return this.logColor; } 
            set { this.logColor = value; } 
        }

        protected int health = 10;
        public virtual int Health 
        {
            get { return this.health; }
            set { this.health = value; }
        }

        protected int attack = 5;
        public virtual int Attack 
        {
            get { return this.attack; }
            set { this.attack = value; }
        }

        protected int defence = 3;
        public virtual int Defence 
        {
            get { return this.defence; }
            set { this.defence = value; }
        }

        public virtual bool isDead()
        {
            return this.Health <= 0;
        }

        public override string ToString()
        {
            return "AbstractUnit";
        }

        public virtual void Turn(IUnit attackedUnit)
        {
            if (this.Attack > 0 && attackedUnit != null)
                attackedUnit.TakeDamage(this.Attack, this);
        }

        public virtual void TakeDamage(int damage, IUnit attacker)
        {
            int takingDamage = this.calculateTakingDamage(damage);

            GameLogger.Instance.Log(
                String.Format("[{0}] наносит {1} урона [{2}] ({3} -> {4})", attacker.ToString(), takingDamage, this.ToString(),
                    this.Health, this.Health - takingDamage),
                GameLogger.LOG_LEVEL__ATTACK
            );

            this.Health -= takingDamage;
            if (this.isDead())
            {
                GameLogger.Instance.Log(String.Format("[{0}] погибает", this.ToString()), GameLogger.LOG_LEVEL__DEATH);
                this.NotifyObservers(this);
            }
        }

        public virtual IUnit GetTarget(IArmy fromArmy)
        {
            return fromArmy.GetUnit();
        }

        protected int calculateTakingDamage(int damage)
        {
            return damage * damage / (damage + this.Defence);
        }

        public void RegisterObserver(IObserver observer)
        {
            Observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            Observers.Remove(observer);
        }

        public void NotifyObservers(object obj)
        {
            foreach (var observer in Observers)
            {
                observer.Update(obj);
            }
        }
    }
}
