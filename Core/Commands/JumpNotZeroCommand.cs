using ProcessorEmulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulator.Commands
{
    internal class JumpNotZeroCommand: Command
    {
        public JumpNotZeroCommand(uint binary) 
        {
            data = binary;
        }

        public JumpNotZeroCommand(AddressingType addressing, uint operandTwo)
        {
            Type = CommandType.JNZ;
            AddressingTwo = addressing;
            OperandTwo = operandTwo;

        }

        public override void Execute(Processor processor)
        {
            if (processor.ZeroFlag)
                return;
            int value = processor.GetOperandValue(AddressingTwo, OperandTwo);
            processor.commandPointer = (uint)value;
        }
    }
}
