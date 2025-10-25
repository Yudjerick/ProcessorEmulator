using ProcessorEmulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulator.Commands
{
    public class JumpZeroCommand: Command
    {
        public JumpZeroCommand(uint binary)
        {
            data = binary;
        }

        public JumpZeroCommand(AddressingType addressing, uint operandTwo)
        {
            Type = CommandType.JZ;
            AddressingTwo = addressing;
            OperandTwo = operandTwo;
        }

        public override void Execute(Processor processor)
        {
            if (!processor.ZeroFlag)
                return;
            uint value = (uint)processor.GetOperandValue(AddressingTwo, OperandTwo);
            processor.commandPointer = value;
        }
    }
}
