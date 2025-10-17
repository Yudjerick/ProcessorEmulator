using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulator.Commands
{
    public class ReturnCommand: Command
    {
        public ReturnCommand() 
        {
            Type = CommandType.RETURN;
        }

        public override void Execute(Processor processor)
        {
            processor.processFinished = true;
        }
    }
}
