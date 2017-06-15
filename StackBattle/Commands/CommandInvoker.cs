using StackBattle.Armies;
using StackBattle.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackBattle.Commands
{
    public class CommandInvoker
    {
        private Stack<Command> UndoStack = new Stack<Command>();
        private Stack<Command> RedoStack = new Stack<Command>();

        public void Undo()
        {
            if (this.UndoStack.Count > 0)
            {
                GameLogger.Instance.Log("[Undoing last turn]", GameLogger.LOG_LEVEL__SUCCESS);
                Command command = this.UndoStack.Pop();
                command.Undo();
                RedoStack.Push(command);
            }
            else
            {
                GameLogger.Instance.Log("[Nothing to undo]", GameLogger.LOG_LEVEL__ERROR);
            }
        }

        public void Redo()
        {
            if (this.RedoStack.Count > 0)
            {
                GameLogger.Instance.Log("[Redo last turn]", GameLogger.LOG_LEVEL__SUCCESS);
                Command command = this.RedoStack.Pop();
                command.Redo();
                UndoStack.Push(command);
            }
            else
            {
                GameLogger.Instance.Log("[Nothing to redo]", GameLogger.LOG_LEVEL__ERROR);
            }
        }

        public void Execute(Command c)
        {
            c.Execute();
            UndoStack.Push(c);
        }

        public void clearRedoStack()
        {
            RedoStack.Clear();
        }
    }
}
