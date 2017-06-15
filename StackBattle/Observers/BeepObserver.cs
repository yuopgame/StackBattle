using System;

namespace StackBattle.Observers
{
    class BeepObserver : IObserver
    {
        public void Update(object obj)
        {
            Console.Beep();
        }
    }
}
