using System;
using System.Collections.Generic;

namespace StackBattle.Logger
{
    public class GameLogger
    {
        public const ConsoleColor LOG_LEVEL__ATTACK          = ConsoleColor.Red;
        public const ConsoleColor LOG_LEVEL__INFO            = ConsoleColor.White;
        public const ConsoleColor LOG_LEVEL__SPECIAL_ABILITY = ConsoleColor.DarkYellow;
        public const ConsoleColor LOG_LEVEL__DEATH           = ConsoleColor.Red;
        public const ConsoleColor LOG_LEVEL__SUCCESS         = ConsoleColor.Green;
        public const ConsoleColor LOG_LEVEL__ERROR           = ConsoleColor.Red;


        //<singleton>
        private static Lazy<GameLogger> _instance = new Lazy<GameLogger>(() => new GameLogger());
        public static GameLogger Instance
        {
            get { return _instance.Value; }
        }

        public void Log(string msg, ConsoleColor logLevel = GameLogger.LOG_LEVEL__INFO)
        {
            Console.ForegroundColor = logLevel;
            Console.WriteLine(msg);
        }
    }
}
