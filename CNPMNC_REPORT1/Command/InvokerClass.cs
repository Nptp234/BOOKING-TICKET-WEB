using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPMNC_REPORT1.Command
{
    public class InvokerClass
    {
        private CommandInterface command;

        public void SetCommand(CommandInterface command)
        {
            this.command = command;
        }

        public void ExecuteCommand()
        {
            if (command != null)
            {
                command.ExecuteCommand();
            }
        }
    }
}
