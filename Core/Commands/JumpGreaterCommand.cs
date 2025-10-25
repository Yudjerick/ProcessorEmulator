using ProcessorEmulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulator.Commands
{
    internal class JumpGreaterCommand: Command
    {
        public JumpGreaterCommand(uint binary)
        {
            data = binary;
        }

        public JumpGreaterCommand(AddressingType addressing, uint operandTwo)
        {
            Type = CommandType.JG;
            AddressingTwo = addressing;
            OperandTwo = operandTwo;
        }

        public override void Execute(Processor processor)
        {
            if (processor.SignFlag || processor.ZeroFlag)
                return;
            int value = processor.GetOperandValue(AddressingTwo, OperandTwo);
            processor.commandPointer = (uint)value;
        }
    }
}
