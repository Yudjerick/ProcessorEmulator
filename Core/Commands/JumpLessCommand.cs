using ProcessorEmulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulator.Commands
{
    public class JumpLessCommand: Command
    {
        public JumpLessCommand(uint binary)
        {
            data = binary;
        }

        public JumpLessCommand(AddressingType addressing, uint operandTwo)
        {
            Type = CommandType.JL;
            AddressingTwo = addressing;
            OperandTwo = operandTwo;
        }

        public override void Execute(Processor processor)
        {
            if (!processor.SignFlag)
                return;
            int value = processor.GetOperandValue(AddressingTwo, OperandTwo);
            processor.commandPointer = (uint)value;
        }
    }
}
