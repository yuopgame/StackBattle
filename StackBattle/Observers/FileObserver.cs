using StackBattle.Unit;
using System;

namespace StackBattle.Observers
{
    class FileObserver : IObserver
    {

        public void Update(object obj)
        {
            IUnit unit = obj as IUnit;
            if (unit == null) return;

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter("death.log", true))
            {
                string text = String.Format("{0} умирает", unit.ToString());
                file.WriteLine(text);
            }  
        }
    }
}
