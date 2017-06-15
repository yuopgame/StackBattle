using StackBattle.FightStrategies;
using StackBattle.Logger;
using System;

namespace StackBattle.Engine
{
    class UI
    {
        public static void PrintMenu()
        {
            GameLogger.Instance.Log("\nPress 'n' - следующий ход, 'u' - отменить ход, 'r' - повторить ход, \n'p' - показать состав армий, 's' - выбрать стратегию сражения\n");
        }

        public static void DoAction()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.N: GameEngine.Instance.Turn(); break;
                case ConsoleKey.P: GameEngine.Instance.PrintArmies(); break;
                case ConsoleKey.U: GameEngine.Instance.CommandInvoker.Undo(); break;
                case ConsoleKey.R: GameEngine.Instance.CommandInvoker.Redo(); break;
                case ConsoleKey.S: UI.ChangeFightStrategy(); break;
                default: Console.WriteLine("\nInvalid key"); break;
            }
        }

        public static void ChangeFightStrategy()
        {
            GameLogger.Instance.Log("Введите номер стратегии сражения:");
            GameLogger.Instance.Log("1: Сражение 1 на 1\n2: Сражение стенка на стенку\n3:Сражение 3 на 3\nEsc: Отмена");
            
            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1: GameEngine.Instance.ChangeFightStrategy(new StackFightStrategy()); break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2: GameEngine.Instance.ChangeFightStrategy(new WallFightStrategy()); break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3: GameEngine.Instance.ChangeFightStrategy(new ThreeByThreeFightStrategy()); break;
                case ConsoleKey.Escape: break;
                default: Console.WriteLine("\nInvalid key"); break;
            }
        }
    }
}
