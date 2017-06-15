using System;
using System.Collections.Generic;
using StackBattle.Armies;
using StackBattle.Logger;
using SpecialUnits;
using StackBattle.Commands;
using StackBattle.FightStrategies;
using StackBattle.Observers;

namespace StackBattle.Engine
{
    /*
     * Основной класс игры (синглтон). Ответственный за старт, основную механику, конец игры.
     */
    public sealed class GameEngine
    {
        //<singleton>
        private static GameEngine _instance;
        public static GameEngine Instance
        {
            get { return _instance ?? (_instance = new GameEngine()); }
            set { _instance = value; }
        }
        // </singleton>

        // <armies>
        public int ArmiesCost { get; set; }
        private IArmy army1 = null;
        private IArmy army2 = null;
        // </armies>

        // <game variables>
        public bool GameStarted { get; set; }

        private Random random = new Random();
        public Random Random { get { return this.random; } }

        private CommandInvoker commandInvoker = new CommandInvoker();
        public CommandInvoker CommandInvoker { get { return this.commandInvoker; } }
        // </game variables>

        private GameEngine()
        {
            GameLogger logger = GameLogger.Instance;
            this.GameStarted = false;
        }

        public void StartGame()
        {
            this.CreateArmies(/*this.ArmiesCost*/50);
            this.GameStarted = true;
            this.Cycle();
            Console.ReadKey();
        }

        public void Cycle()
        {
            int winner = 0;
            while (winner == 0)
            {
                UI.PrintMenu();
                UI.DoAction();
                
                if (army1.IsDefeated())
                {
                    winner = 1;
                }
                else if (army2.IsDefeated())
                {
                    winner = 2;
                }
            }
            GameLogger.Instance.Log(String.Format("Побеждает армия №{0}", winner), GameLogger.LOG_LEVEL__SUCCESS);
        }

        public void PrintArmies()
        {
            GameLogger.Instance.Log("*** Состав первой армии ***");
            army1.Log();
            GameLogger.Instance.Log("\n*** Состав второй армии ***");
            army2.Log();
        }

        public void Turn()
        {
            this.CommandInvoker.clearRedoStack();

            TurnCommand command = new TurnCommand(army1, army2);
            this.CommandInvoker.Execute(command);
        }

        public void ChangeFightStrategy(IFightStrategy strategy)
        {
            this.army1.FightStrategy = strategy;
            this.army2.FightStrategy = strategy;
        }

        private void CreateArmies(int cost)
        {
            this.army1 = this.CreateArmy(cost);
            this.army2 = this.CreateArmy(cost);
        }

        private IArmy CreateArmy(int cost)
        {
            IArmy army = ArmyGenerator.GenerateArmy(cost);

            List<IObserver> observers = new List<IObserver>(){
                new BeepObserver(),
                new FileObserver()
            };

            this.AddObservers(army, observers);
            return army;
        }

        private void AddObservers(IArmy army, List<IObserver> observers)
        {
            foreach (var unit in army.Units)
            {
                foreach (var observer in observers)
                {
                    unit.RegisterObserver(observer);
                }
            }
        }
    }
}
